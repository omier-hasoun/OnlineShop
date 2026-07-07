
using Infrastructure.Common.Abstractions;
using Infrastructure.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;

namespace Api.Minimals;

public static class StripeWebhookEndpoints
{
    public static IEndpointRouteBuilder MapStripeWebhookEndpoints(this IEndpointRouteBuilder app)
    {
        // Stripe webhook endpoints must return a response quickly.
        app.MapPost("stripe/webhooks", StripeWebhook);
        return app;
    }


    private static async Task<IResult> StripeWebhook(
        AppDbContext db,
        HttpContext context,
        IConfiguration config,
        [FromKeyedServices("Snowflake")] IPrimitiveTypeIdGenerator<long> idGen,
        TimeProvider time,
        ILogger<Program> logger)
    {
        var json = await new StreamReader(context.Request.Body).ReadToEndAsync();
        Event? stripeEvent = null;
        try
        {
            stripeEvent = EventUtility.ConstructEvent(
                json,
                context.Request.Headers["Stripe-Signature"],
                config["STRIPE_WHS"]);

            if (stripeEvent.Type == EventTypes.CheckoutSessionCompleted)
            {
                if (stripeEvent.Data.Object is not Session session ||
                    string.IsNullOrWhiteSpace(session.Id))
                {
                    logger.LogWarning(
                        "Invalid CheckoutSessionCompleted payload. EventId: {EventId}",
                        stripeEvent.Id);

                    return Results.BadRequest();
                }

                db.StripeEvents.Add(new Infrastructure.Data.Models.StripeEvent()
                {
                    Id = idGen.Generate(),
                    ProcessedAt = null,
                    ReceivedAt = time.GetUtcNow().UtcDateTime,
                    Status = Infrastructure.Data.Models.StripeEventState.Pending,
                    StripeEventId = stripeEvent.Id,
                    Type = stripeEvent.Type,
                    StripeSessionId = session.Id
                });

                await db.SaveAsync();
            }



            return Results.Ok();
        }
        catch (DbUpdateException ex) 
        when ( ex.InnerException is SqlException sqlEx &&(sqlEx.Number == 2601 || sqlEx.Number == 2627))
        {
            logger.LogInformation(
                "Duplicate Stripe event received: {EventId}",
                stripeEvent!.Id);

            return Results.Ok();
        }
        catch (StripeException ex)
        {
            logger.LogWarning(
                "Stripe webhook rejected: {Message}",
                ex.Message);

            return Results.BadRequest();
        }
    }
    //private static CheckoutCompletedCommand MapToCheckoutCommand(Session session)
    //{
    //    var shipping = session.CollectedInformation?.ShippingDetails?.Address;
    //    var billing = session.CustomerDetails?.Address;

    //    var shippingAddress = shipping != null
    //        ? new ShippingAddressDto(session.CustomerDetails?.Name, session.CustomerDetails?.Phone, shipping.Country, null, shipping.City, shipping.PostalCode, shipping.Line1, shipping.Line2, shipping.State)
    //        : null;

    //    var billingAddress = billing != null
    //        ? new BillingAddressDto(session.CustomerDetails?.Name, session.CustomerDetails?.Phone, billing.Country, null, billing.City, billing.PostalCode, billing.Line1, billing.Line2, billing.State)
    //        : null;

    //    var paymentMethodType = session.PaymentIntent?.PaymentMethod?.Type;

    //    string? paymentFingerprint = null;

    //    var pm = session.PaymentIntent?.PaymentMethod;

    //    if (paymentMethodType != null && pm != null)
    //    {

    //        switch (paymentMethodType)
    //        {
    //            case "card" when pm.Card != null:
    //                // Unique string representing the physical card asset
    //                paymentFingerprint = pm.Card.Fingerprint;
    //                break;

    //            case "paypal" when pm.Paypal != null:
    //                // Unique string representing the buyer's PayPal account
    //                paymentFingerprint = pm.Paypal.PayerId;
    //                break;

    //            case "link" when pm.Link != null:
    //                // Use the Link persistent account token if available, otherwise fallback
    //                paymentFingerprint = pm.Link.Email;
    //                break;
    //        }
    //    }

    //    Guid? userId = Guid.TryParse(session.ClientReferenceId, out var parsedGuid) ? parsedGuid : null;

    //    var lines = session.LineItems?.Select(x => new CheckoutLineDto(Convert.ToInt64(x.Id),
    //                                                                       x.Price.Product.Url,
    //                                                                       x.Price.UnitAmount,
    //                                                                       x.Price.Product.Name,
    //                                                                       Convert.ToInt16(x.Quantity))).ToList();
    //    return new CheckoutCompletedCommand(
    //        UserId: userId,
    //        BillingAddress: billingAddress,
    //        ShippingAddress: shippingAddress,
    //        CustomerEmail: session.CustomerDetails?.Email,
    //        ShippingCost: session.ShippingCost?.AmountTotal ?? 0,
    //        SubTotal: session.AmountSubtotal ?? 0,
    //        Total: session.AmountTotal ?? 0,

    //        PaymentMethod: paymentMethodType,
    //        PaymentStatus: session.PaymentStatus,
    //        PaymentFingerPrint: paymentFingerprint,
    //        ProviderPaymentId: session.PaymentIntentId,
    //        lines

    //    );
    //}
}
