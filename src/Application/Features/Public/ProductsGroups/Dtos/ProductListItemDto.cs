using Domain.Common.ValueObjects;
using Domain.ProductGroups.Products;
using Domain.ProductGroups.ValueObjects;

namespace Application.Features.Public.ProductsGroups.Dtos;

public sealed record ProductListItemDto
{
    public long ProductGroupId { get; init; }
    public long ProductId { get; }
    public string Title { get; init; } = null!;
    public double Price { get; init; } 
    public string Brand { get; init; } = null!;
    public float AverageRating { get; init; }
    public string? Image { get; init; }
    public byte? DiscountPercentage { get; init; }
    public double? PriceAfterDiscount { get; init; }
    public bool HasActiveDiscount { get; init; }
    public bool IsAvailable { get; }

    public ProductListItemDto(ProductGroupId productGroupId, ProductId productId, string title, Money price, string brand, ProductAverageRating averageRating,
        ProductImage? displayImage, bool hasActiveDiscount, Money? priceAfterDiscount, byte? discountPercentage, bool isAvailable)
    {
        ProductGroupId = productGroupId.Value;
        ProductId = productId.Value;
        Title = title;
        Brand = brand;
        HasActiveDiscount = hasActiveDiscount;
        IsAvailable = isAvailable;
        Price = (double)price.Value;
        PriceAfterDiscount = hasActiveDiscount ? (double)priceAfterDiscount!.Value : null;
        Image = displayImage?.FileName;
        AverageRating = averageRating.GetRoundedValue();
        DiscountPercentage = hasActiveDiscount ? discountPercentage : null;
    }
}
