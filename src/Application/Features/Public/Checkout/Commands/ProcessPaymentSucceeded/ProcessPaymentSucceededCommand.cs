namespace Application.Features.Public.Checkout.Commands.ProcessPaymentSucceeded;

public sealed record ProcessPaymentSucceededCommand(string SessionId) : IRequest
{
}
