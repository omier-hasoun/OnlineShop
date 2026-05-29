
using Application.Common.Dtos;
using Domain.Carts.CartItems;

namespace Application.Features.Public.Carts.Commands.UpdateCartItem;

public sealed record UpdateCartItemCommand(UserIdentity CartIdentity, long CartItemId, short Quantity) :IRequest<Result<Updated>>
{
    internal CartItemId ParsedCartItemId => new (CartItemId);
}
