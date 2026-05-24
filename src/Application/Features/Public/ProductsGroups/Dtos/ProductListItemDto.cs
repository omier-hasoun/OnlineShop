using Domain.Common.ValueObjects;
using Domain.ProductGroups.ValueObjects;

namespace Application.Features.Public.ProductsGroups.Dtos;
/// <summary>
/// This Dto used to for listing Products when requested
/// </summary>
/// <param name="VariantId"></param>
/// <param name="ProductId"></param>
/// <param name="Title"></param>
/// <param name="PriceNow"></param>
/// <param name="Brand"></param>
/// <param name="AverageRating"></param>
/// <param name="Images"></param>
/// <param name="DiscountPercentage"></param>
/// <param name="OriginalPrice"></param>
public sealed record ProductListItemDto
{
    public string Id { get; init; }
    public string Title { get; init; } = null!;
    public double Price { get; init; } 
    public string Brand { get; init; } = null!;
    public float AverageRating { get; init; }
    public string? ImageFileName { get; init; }
    public byte? DiscountPercentage { get; init; }
    public double? PriceBeforeDiscount { get; init; }

    public ProductListItemDto(ProductGroupId id, string title, Money? priceBeforeDiscount, string brand, ProductAverageRating avgerageRating, Money priceNow, ProductImage? primaryImage, byte? discountPercentage)
    {
        Id = id.ToString();
        Title = title;
        Brand = brand;
        PriceBeforeDiscount = priceBeforeDiscount is null ? null : (double)priceBeforeDiscount.Value;
        Price = (double)priceNow.Value;
        ImageFileName = primaryImage?.FileName;
        AverageRating = avgerageRating.Value;
        DiscountPercentage = discountPercentage is null || discountPercentage == 0 ? null : discountPercentage;
    }
}
