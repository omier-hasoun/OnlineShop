
using Domain.ProductGroups.Products;

namespace Application.Features.Public.Checkout.Dtos;

public sealed record OrderLineDetailsDto
{
    public OrderLineDetailsDto(long? productId, string? thumnailUrl, long? unitPrice, string? productTitle, short? quantity)
    {
        ProductId = productId;
        ProductThumbnailUrl = thumnailUrl;
        UnitPrice = unitPrice;
        ProductName = productTitle;

        Quantity = quantity;
    }

    public long? ProductId { get; }
    public string? ProductThumbnailUrl { get; }
    public string? ProductName { get; }
    public long? UnitPrice { get; }

    public short? Quantity { get; }

}
