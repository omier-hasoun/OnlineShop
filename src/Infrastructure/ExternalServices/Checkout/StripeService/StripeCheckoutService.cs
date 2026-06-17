using System.Net;
using Application.Features.Public.Checkout.Dtos;
using Infrastructure.Common.Exceptions;
using Microsoft.Extensions.Logging;
using Stripe;
using Stripe.Checkout;

#nullable disable

namespace Infrastructure.ExternalServices.Checkout.StripeService;

internal sealed class StripeCheckoutService: ICheckoutProvider
{
    private readonly string _apiKey;
    private readonly ILogger<StripeCheckoutService> _logger;

    public StripeCheckoutService(IConfiguration config, ILogger<StripeCheckoutService> logger)
    {
        _apiKey = config["STRIPE_TEST_KEY"] ?? throw new StripeApiKeyWasNotProvidedException();
        _logger = logger;
    }

    public async Task<string> BeginCheckout(CheckoutSessionInfo info, CancellationToken ct)
    {
        
        var client = new StripeClient(_apiKey);

        var lineItems = info.OrderItemsDetails
                            .Select(item => new SessionLineItemOptions
                            {
                                Quantity = item.Quantity,
                                AdjustableQuantity = new SessionLineItemAdjustableQuantityOptions
                                {
                                    Enabled = false
                                },
                                PriceData = new SessionLineItemPriceDataOptions
                                {
                                    Currency = info.CurrencyCode,

                                    ProductData = new SessionLineItemPriceDataProductDataOptions
                                    {
                                        Name = item.ProductTitle
                                    }
                                }
                            })
                            .ToList();

        var sessionOptions = new SessionCreateOptions
        {

            Mode = "payment",
            SuccessUrl = "https://localhost:7039/scalar",
            CancelUrl = "https://localhost:7039/scalar",

            CustomerEmail = info.CustomerEmail,
            BillingAddressCollection = "required",

            ShippingAddressCollection = new SessionShippingAddressCollectionOptions
            {
                AllowedCountries = ["US", "DE", "LB", "EG", "IQ", "SE", "GB", "SA", "JO"],
            },

            PhoneNumberCollection = new SessionPhoneNumberCollectionOptions
            {
                Enabled = true,
            },

            
            NameCollection = new SessionNameCollectionOptions
            {
                Business = new SessionNameCollectionBusinessOptions
                {
                    Enabled = true,
                    Optional = true,
                }
            },

            ClientReferenceId = info.ReferenceId,

            LineItems = lineItems,
            
            

            ShippingOptions = new List<SessionShippingOptionOptions>
            {

                new SessionShippingOptionOptions
                {

                    ShippingRateData = new SessionShippingOptionShippingRateDataOptions
                    {
                        DisplayName = "Standard Shipping",
                        
                        Type = "fixed_amount",
                        
                        FixedAmount = new SessionShippingOptionShippingRateDataFixedAmountOptions
                        {
                            Amount = 599, // €5.99 in cents
                            Currency = info.CurrencyCode
                        },
                        
                        DeliveryEstimate = new SessionShippingOptionShippingRateDataDeliveryEstimateOptions
                        {
                            Minimum =
                                new SessionShippingOptionShippingRateDataDeliveryEstimateMinimumOptions
                                {
                                    Unit = "business_day",
                                    Value = 3
                                },
                        
                            Maximum =
                                new SessionShippingOptionShippingRateDataDeliveryEstimateMaximumOptions
                                {
                                    Unit = "business_day",
                                    Value = 5
                                }
                        }
                    
                    }
                }

            },

            
            UiMode = "hosted_page"
        };



        try
        {
            var session = await client.V1.Checkout.Sessions.CreateAsync(sessionOptions, null, ct);
            _logger.LogInformation(session.Url);
            return session.Url;
        }
        catch
        {
            throw;
        }
        
    }
}
