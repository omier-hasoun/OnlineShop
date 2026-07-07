
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
    
}
