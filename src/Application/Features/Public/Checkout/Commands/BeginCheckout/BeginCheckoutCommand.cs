
using Application.Common.Dtos;

namespace Application.Features.Public.Checkout.Commands.BeginCheckout;

public sealed record BeginCheckoutCommand(UserIdentity UserIdentity) : IRequest<Result<string>>
{
}
