
using Application.Common.Dtos;
using Domain.Carts.CartItems;

namespace Application.Features.Public.Carts.Commands.UpdateCartItem;

public sealed record UpdateCartItemCommand(CurrentUser CartIdentity, long CartItemId, short Quantity) :IRequest<Result<Updated>>
{
    internal CartItemId ParsedCartItemId => new (CartItemId);
}
