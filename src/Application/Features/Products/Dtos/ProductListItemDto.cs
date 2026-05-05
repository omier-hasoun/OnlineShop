
using Domain.Common.ValueObjects;
using Domain.Products.ValueObjects;

namespace Application.Features.Products.Dtos;
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
    public long Id { get; init; }
    public string Title { get; init; } = null!;
    public decimal PriceNow { get; init; } 
    public string Brand { get; init; } = null!;
    public float AverageRating { get; init; }
    public string Image { get; init; } = null!;
    public byte DiscountPercentage { get; init; }
    public decimal OriginalPrice { get; init; }

    public ProductListItemDto(ProductId id, string title, Money originalPrice, string brand, ProductAverageRating avgerageRating, Money priceNow, ProductImage image, byte discountPercentage)
    {
        Id = id.Value;
        Title = title;
        Brand = brand;
        OriginalPrice = originalPrice.Value;
        PriceNow = priceNow.Value;
        Image = image.FileName;
        AverageRating = avgerageRating.Value;
        DiscountPercentage = discountPercentage;
    }
}
