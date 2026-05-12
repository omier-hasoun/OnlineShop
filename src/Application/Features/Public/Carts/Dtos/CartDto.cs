
using Domain.Carts;

namespace Application.Features.Public.Carts.Dtos;

public sealed class CartDto
{
    public long Id { get; }
    public List<CartItemDto> Items { get; }

    public CartDto(CartId id, List<CartItemDto> items)
    {
        Id = id.Value;
        Items = items;
    }
}
