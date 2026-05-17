
using Application.Common.Dtos;
using Domain.Carts.CartItems;

namespace Application.Features.Public.Carts.Commands.UpdateCartItem;

public sealed record UpdateCartItemCommand(CartIdentity CartIdentity, long CartItemId, short Quantity) :IRequest<Result<Updated>>
{
    public CartItemId ParsedCartItemId => new CartItemId(CartItemId);
}
