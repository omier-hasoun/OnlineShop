
using Domain.Common.ValueObjects;
using Domain.ProductsGroups.Products;
using Domain.ProductsGroups.ValueObjects;

namespace Application.Features.Management.ProductGroups.Dtos;

public sealed record ProductListItemDto
{
    public string Id { get; init; }
    public string Title { get; init; } = null!;
    public double Price { get; init; }
    public string Brand { get; init; } = null!;
    public string Status { get; init; }
    public float AverageRating { get; init; }
    public string? ImageFileName { get; init; }
    public byte? DiscountPercentage { get; init; }
    public double? PriceBeforeDiscount { get; init; }

    public ProductListItemDto(ProductsGroupId id, string title, Money? priceBeforeDiscount,
        string brand, ProductAverageRating avgerageRating, Money priceNow, ProductImage? primaryImage,
        byte? discountPercentage, ProductsGroupStatus productsGroupStatus)
    {
        Id = id.ToString();
        Title = title;
        Brand = brand;
        Status = productsGroupStatus.ToString();
        PriceBeforeDiscount = priceBeforeDiscount is null ? null : (double)priceBeforeDiscount.Value;
        Price = (double)priceNow.Value;
        ImageFileName = primaryImage?.FileName;
        AverageRating = avgerageRating.Value;
        DiscountPercentage = discountPercentage is null || discountPercentage == 0 ? null : discountPercentage;
    }
}
