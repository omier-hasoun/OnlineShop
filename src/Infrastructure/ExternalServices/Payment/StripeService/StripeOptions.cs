namespace Infrastructure.ExternalServices.Payment.StripeService;

public sealed class StripeOptions
{
    public const string SectionName = "Stripe";
    public string TestKey { get; set; } = null!;
}
