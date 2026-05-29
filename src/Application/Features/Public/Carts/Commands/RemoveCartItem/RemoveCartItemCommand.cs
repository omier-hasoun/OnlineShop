
using Application.Common.Dtos;
using Domain.Carts.CartItems;

namespace Application.Features.Public.Carts.Commands.RemoveCartItem;

public sealed record RemoveCartItemCommand(long CartItemId, UserIdentity CartIdentity) : IRequest<Result<Deleted>>
{
    internal CartItemId ParsedCartItemId => new(CartItemId);
}
