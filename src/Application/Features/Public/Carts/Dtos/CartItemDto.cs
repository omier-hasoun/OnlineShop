
using Domain.Carts.CartItems;

namespace Application.Features.Public.Carts.Dtos;

public sealed record CartItemDto
{
    public CartItemDto(CartItemId id, short quantity, ProductCartItemDto? product)
    {
        Id = id.Value;
        Quantity = quantity;
        Product = product;
    }

    public long Id { get; }
    public short Quantity { get; }
    public ProductCartItemDto? Product { get; }
}
