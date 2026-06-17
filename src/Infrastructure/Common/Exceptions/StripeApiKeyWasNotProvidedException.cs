

namespace Infrastructure.Common.Exceptions;

internal sealed class StripeApiKeyWasNotProvidedException : Exception
{
    public StripeApiKeyWasNotProvidedException(string message = "no Stripe api key was provided") : base(message)
    {

    }
}
