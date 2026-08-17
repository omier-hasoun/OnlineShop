using Application.Common.Dtos;

namespace Application.Features.Public.Checkout.Commands.ProceedToPayment;

public sealed record ProceedToPaymentCommand(CurrentUser UserIdentity) : IRequest<Result<string>>
{
}
