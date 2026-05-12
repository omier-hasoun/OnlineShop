
using Domain.Common.ValueObjects;
using Domain.Products.ValueObjects;

namespace Application.Features.Management.Products.Dtos;

public sealed record ProductListItemDto
{
    public long Id { get; init; }
    public string Title { get; init; } = null!;
    public double Price { get; init; }
    public string Brand { get; init; } = null!;
    public string Status { get; init; }
    public float AverageRating { get; init; }
    public string? ImageFileName { get; init; }
    public byte? DiscountPercentage { get; init; }
    public double? PriceBeforeDiscount { get; init; }

    public ProductListItemDto(ProductId id, string title, Money? priceBeforeDiscount, string brand, ProductAverageRating avgerageRating, Money priceNow, ProductImage? primaryImage, byte? discountPercentage, ProductStatus productStatus)
    {
        Id = id.Value;
        Title = title;
        Brand = brand;
        Status = productStatus.ToString();
        PriceBeforeDiscount = priceBeforeDiscount is null ? null : (double)priceBeforeDiscount.Value;
        Price = (double)priceNow.Value;
        ImageFileName = primaryImage?.FileName;
        AverageRating = avgerageRating.Value;
        DiscountPercentage = discountPercentage is null || discountPercentage == 0 ? null : discountPercentage;
    }
}
