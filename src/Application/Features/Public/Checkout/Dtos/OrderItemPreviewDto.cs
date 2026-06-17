
using Domain.Common.ValueObjects;
using Domain.ProductGroups.Products;
using Domain.ProductGroups.ValueObjects;

namespace Application.Features.Public.Checkout.Dtos;

public sealed record OrderItemPreviewDto
{
    public OrderItemPreviewDto(ProductId productId, ProductImage? image, string productTitle, bool hasActiveDiscount, byte? discountPercentage, Money? originalPrice,
        Money? priceAfterDiscount, Money currentPrice, Money totalPrice, short quantity)
    {
        ProductId = productId.Value;
        Image = image?.FileName;
        ProductTitle = productTitle;
        HasActiveDiscount = hasActiveDiscount;
        DiscountPercentage = discountPercentage;
        OriginalPrice = originalPrice?.Value;
        CurrentPrice = currentPrice.Value;
        TotalPrice = totalPrice.Value;

        PriceAfterDiscount = priceAfterDiscount?.Value;
        Quantity = quantity;
    }

    public long ProductId { get; }
    public string? Image { get; }
    public string ProductTitle { get; }
    public bool HasActiveDiscount { get; }
    public byte? DiscountPercentage { get; }
    public decimal? PriceAfterDiscount { get; }
    public decimal? OriginalPrice { get; }
    public decimal CurrentPrice { get; }
    public decimal TotalPrice { get; }

    public short Quantity { get; }

}
