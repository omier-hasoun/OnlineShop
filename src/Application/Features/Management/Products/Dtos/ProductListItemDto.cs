
using Domain.Common.ValueObjects;
using Domain.Products.ValueObjects;

namespace Application.Features.Management.Products.Dtos;

public sealed record ProductListItemDto
{
    public long Id { get; init; }
    public string Title { get; init; } = null!;
    public double Price { get; init; }
    public string Brand { get; init; } = null!;
    public string Status { get; }
    public float Average_Rating { get; init; }
    public string? Image_File_Name { get; init; }
    public byte? Discount_Percentage { get; init; }
    public double? Price_Before_Discount { get; init; }

    public ProductListItemDto(ProductId id, string title, Money? priceBeforeDiscount, string brand, ProductAverageRating avgerageRating, Money priceNow, ProductImage? primaryImage, byte? discountPercentage, ProductStatus productStatus)
    {
        Id = id.Value;
        Title = title;
        Brand = brand;
        Status = productStatus.ToString();
        Price_Before_Discount = priceBeforeDiscount is null ? null : (double)priceBeforeDiscount.Value;
        Price = (double)priceNow.Value;
        Image_File_Name = primaryImage?.FileName;
        Average_Rating = avgerageRating.Value;
        Discount_Percentage = discountPercentage is null || discountPercentage == 0 ? null : discountPercentage;
    }
}
