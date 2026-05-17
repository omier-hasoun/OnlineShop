
using Application.Common.Dtos;
using Domain.Carts.CartItems;

namespace Application.Features.Public.Carts.Commands.RemoveCartItem;

public sealed record RemoveCartItemCommand(long CartItemId, CartIdentity CartIdentity) : IRequest<Result<Deleted>>
{
    public CartItemId ParsedCartItemId => new CartItemId(CartItemId);
}
