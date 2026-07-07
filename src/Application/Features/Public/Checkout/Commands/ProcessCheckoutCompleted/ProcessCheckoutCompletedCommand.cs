namespace Application.Features.Public.Checkout.Commands.ProcessCheckoutCompleted;

public sealed record ProcessCheckoutCompletedCommand(string SessionId) : IRequest
{
}
