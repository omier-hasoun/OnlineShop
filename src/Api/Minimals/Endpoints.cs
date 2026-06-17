
using Stripe;
using Stripe.Checkout;


namespace Api.Minimals;

public static class Endpoints
{
    public static bool Configured = false;
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("stripe/webhooks", StripeWebhook);
        return app;
    }

    public static async Task<IResult> StripeWebhook(ISender sender, IHttpContextAccessor a, ILogger<StripeClient> logger, CancellationToken ct = default)
    {
        var context = a.HttpContext!;
        //var webhookSecret
        //if(Configured is false)
        //{
        //    var options = new WebhookEndpointCreateOptions
        //    {
        //        Url = "https://localhost:7039/stripe/webhooks/session-succeeded",
        //        EnabledEvents = new List<string>
        //    {
        //        "checkout.session.completed",
        //        //"payment_intent.succeeded",
        //        //"invoice.payment_succeeded" // Add events you need
        //    },
        //        Description = "Webhook for payment processing"
        //    };

        //    var service = new WebhookEndpointService();
        //    WebhookEndpoint webhookEndpoint = await service.CreateAsync(options);

        //    // Save the webhook secret - you'll need this to verify events
        //    var webhookSecret = webhookEndpoint.Secret;

        //    Configured = true;
        //}

        var json = await new StreamReader(context.Request.Body).ReadToEndAsync();

        var stripeSignature = context.Request.Headers["Stripe-Signature"];

        try
        {
            var stripeEvent = EventUtility.ConstructEvent(json, stripeSignature, "whsec_73b2807bb2ed52685c5c56c7813f52564b31a9cab9bed4302172c8cbd1cad93b");

            switch(stripeEvent.Type)
            {
                case EventTypes.CheckoutSessionCompleted:

                    var session = stripeEvent.Data.Object as Session;
                    logger.LogInformation($"ah shit here we go again sessionId : {session.Id}");
                    //await sender.Send(Checkout,ct);
                    break;

                case EventTypes.ChargeSucceeded:

                    break;

                case EventTypes.ChargeRefunded:

                    break;
            }

            return Results.Ok();
        }
        catch (StripeException e)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to verify stripe signature: {e.Message}");
            return Results.BadRequest();
        }
    }
 
}
