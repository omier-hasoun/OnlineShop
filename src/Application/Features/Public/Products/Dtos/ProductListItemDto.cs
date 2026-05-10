using Domain.Common.ValueObjects;
using Domain.Products.ValueObjects;

namespace Application.Features.Public.Products.Dtos;
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
    public double Price { get; init; } 
    public string Brand { get; init; } = null!;
    public float Average_Rating { get; init; }
    public string? Image_File_Name { get; init; }
    public byte? Discount_Percentage { get; init; }
    public double? Price_Before_Discount { get; init; }

    public ProductListItemDto(ProductId id, string title, Money? priceBeforeDiscount, string brand, ProductAverageRating avgerageRating, Money priceNow, ProductImage? primaryImage, byte? discountPercentage)
    {
        Id = id.Value;
        Title = title;
        Brand = brand;
        Price_Before_Discount = priceBeforeDiscount is null ? null : (double)priceBeforeDiscount.Value;
        Price = (double)priceNow.Value;
        Image_File_Name = primaryImage?.FileName;
        Average_Rating = avgerageRating.Value;
        Discount_Percentage = discountPercentage is null || discountPercentage == 0 ? null : discountPercentage;
    }
}
