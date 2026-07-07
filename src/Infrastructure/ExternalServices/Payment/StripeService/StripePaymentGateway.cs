using System.Net;
using Application.Features.Public.Checkout.Dtos;
using Domain.Common.ValueObjects;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

#nullable disable

namespace Infrastructure.ExternalServices.Payment.StripeService;

internal sealed class StripePaymentGateway : IPaymentGateway
{
    private readonly StripeClient _client;
    private readonly ILogger<StripePaymentGateway> _logger;
    private readonly TimeProvider _time;

    private static readonly SessionShippingAddressCollectionOptions _shippingOptions = new()
    {
        AllowedCountries = ["US", "DE", "LB", "EG", "IQ", "SE", "GB", "SA", "JO"]
    };

    private static readonly SessionPhoneNumberCollectionOptions _phoneOptions = new() { Enabled = true };

    private static readonly SessionNameCollectionOptions _nameOptions = new()
    {
        Business = new SessionNameCollectionBusinessOptions { Enabled = true, Optional = true }
    };
    private static readonly SessionLineItemAdjustableQuantityOptions _adjustableQuantityDuringCheckout = new()
    {
        Enabled = false
    };
    private static readonly SessionGetOptions _sessionGetOptions = new()
    {
        Expand = new List<string>
    {
        "payment_intent",
        "payment_intent.payment_method",
        "total_details"
    },
        ExtraParams = null,
        

    };

    public StripePaymentGateway(
        IOptions<StripeOptions> stripeOptions,
        ILogger<StripePaymentGateway> logger,
        TimeProvider time)
    {
        _logger = logger;
        _time = time;

        var apiKey = stripeOptions.Value.TestKey;
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new InvalidOperationException("Stripe API key was not provided.");
        }

        _client = new StripeClient(apiKey);
    }

    public async Task<(string SessionId, string SessionUrl)> StartPaymentProcessAsync(OrderDetailsDto details, CancellationToken ct)
    {

        if (details.OrderLines == null || details.OrderLines.Count == 0)
        {
            throw new InvalidOperationException("Cannot create a Checkout session with no items.");
        }

        var lineItems = details.OrderLines.Select(item => new SessionLineItemOptions
        {
            Quantity = item.Quantity,
            AdjustableQuantity = _adjustableQuantityDuringCheckout,
            
            PriceData = new SessionLineItemPriceDataOptions
            {
                
                Currency = details.Currency,
                UnitAmount = item.UnitPrice,
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = item.ProductName,
                    Images = item.ProductThumbnailUrl is null ? null : [item.ProductThumbnailUrl]
                }
                
            }

            
        }).ToList();

        var sessionOptions = new SessionCreateOptions
        {
            Mode = "payment",
            BillingAddressCollection = "required",
            ExpiresAt = _time.GetUtcNow().AddMinutes(240).UtcDateTime,
            UiMode = "hosted_page",


            SuccessUrl = details.SuccessUrl,
            CancelUrl = details.CancelUrl,
            ClientReferenceId = details.OrderId,
            LineItems = lineItems,
            ShippingAddressCollection = _shippingOptions,
            PhoneNumberCollection = _phoneOptions,
            NameCollection = _nameOptions,

            ShippingOptions = [
                new SessionShippingOptionOptions
                {
                    ShippingRateData = new SessionShippingOptionShippingRateDataOptions
                    {
                        DisplayName = "Standard Shipping",
                        Type = "fixed_amount",
                        FixedAmount = new SessionShippingOptionShippingRateDataFixedAmountOptions
                        {
                            Amount = details.ShippingCost,
                            Currency = details.Currency
                        },
                        DeliveryEstimate = new SessionShippingOptionShippingRateDataDeliveryEstimateOptions
                        {
                            Minimum = new SessionShippingOptionShippingRateDataDeliveryEstimateMinimumOptions { Unit = "business_day", Value = 3 },
                            Maximum = new SessionShippingOptionShippingRateDataDeliveryEstimateMaximumOptions { Unit = "business_day", Value = 5 }
                        }
                    }
                }
            ],
        };

        var session = await _client.V1.Checkout.Sessions.CreateAsync(sessionOptions, null, ct);

        return (session.Id, session.Url);
    }

    public async Task<(string RefundId , RefundState Status)> RefundAsync(string paymentId, CancellationToken ct)
    {
        var options = new RefundCreateOptions
        {
            PaymentIntent = paymentId,
            
            Reason = RefundReasons.RequestedByCustomer
        };

        var refund = await _client.V1.Refunds.CreateAsync(options, null, ct);

        var status = refund.Status switch
        {
            "succeeded" => RefundState.Succeeded,
            "failed" => RefundState.Failed,
            "requires_action" => RefundState.ActionRequired,
            "canceled" => RefundState.Canceled,
            _ => throw new InvalidOperationException("Unexpected refund status")
        };

        return (refund.Id, status);
    }

    public async Task CancelPaymentProcess(string sessionId, CancellationToken ct)
    {
        try
        {
            await _client.V1.Checkout.Sessions.ExpireAsync(sessionId, null, null, ct);
        }
        catch
        {

        }
    }

    public async Task<PaymentDetailsDto> GetPaymentDetailsAsync(string sessionId, CancellationToken ct)
    {
        var session = await _client.V1.Checkout.Sessions.GetAsync(
            sessionId,
            _sessionGetOptions,
            null,
            ct) ?? throw new InvalidOperationException($"Session not found: {sessionId}"); ;

        var paymentIntent = session.PaymentIntent
            ?? throw new InvalidOperationException("PaymentIntent missing");

        var paymentMethod = paymentIntent.PaymentMethod
            ?? throw new InvalidOperationException("PaymentMethod missing");

        var shipping = session.CollectedInformation?.ShippingDetails?.Address;

        if(shipping is null)
            throw new InvalidOperationException("ShippingAddress missing");
        

        if(session.CustomerDetails is null)
            throw new InvalidOperationException("CustomerDetails missing");

        var shippingAddress = new AddressDetails(
            session.CollectedInformation.ShippingDetails.Name,
            session.CustomerDetails.Phone,
            shipping.Country,
            null,
            shipping.City,
            shipping.PostalCode,
            shipping.Line1,
            shipping.Line2,
            shipping.State,
            null);


        var billing = session.CustomerDetails.Address;

        var billingAddress = new AddressDetails(
            session.CustomerDetails.Name,
            session.CustomerDetails.Phone,
            billing.Country,
            null,
            billing.City,
            billing.PostalCode,
            billing.Line1,
            billing.Line2,
            billing.State,
            null);

        string fingerprint = paymentMethod.Type switch
        {
            "paypal" => paymentMethod.Paypal?.PayerId,
            "card" => paymentMethod.Card?.Fingerprint,
            "link" => paymentMethod.Link?.Email,
            _ => throw new InvalidOperationException($"Unsupported PaymentMethodType: {paymentMethod.Type}")
        };

        PaymentState paymentStatus = session.PaymentStatus switch
        {
            "paid" => PaymentState.Paid,
            "unpaid" => PaymentState.Unpaid,
            "no_payment_required" => PaymentState.NoPaymentRequired,
            _ => throw new InvalidOperationException($"Unexpected payment status: {session.PaymentStatus}")
        };

        string email = session.CustomerDetails.Email;

        return new PaymentDetailsDto(
            OrderId: session.ClientReferenceId,
            TaxAmount: session.TotalDetails?.AmountTax ?? 0,
            TotalAmount: session.AmountTotal ?? 0,
            PaymentMethodFingerPrint: fingerprint ?? string.Empty,
            PaymentMethodType: paymentMethod.Type,
            PaymentStatus: paymentStatus,
            PaymentId: session.PaymentIntentId, 
            BillingAddress: billingAddress,
            ShippingAddress: shippingAddress,
            Email: email
        );
    }
}
