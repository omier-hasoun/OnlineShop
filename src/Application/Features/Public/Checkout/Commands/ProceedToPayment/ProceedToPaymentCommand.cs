using Application.Common.Dtos;

namespace Application.Features.Public.Checkout.Commands.ProceedToPayment;

public sealed record ProceedToPaymentCommand(UserIdentity UserIdentity) : IRequest<Result<string>>
{
}
