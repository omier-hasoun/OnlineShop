using Application.Common.ResponseModels;
using Domain.Common.ValueObjects;
using Domain.Products.ValueObjects;

namespace Application.Features.Public.Products.Dtos;

public sealed record ProductVariantDto
{
    public double Price { get; init; }
    public double? Price_Before_Discount { get; init; }
    public byte? Discount_Percentage { get; init; }

    public List<ImageDto> Images { get; init; } = [];
    public Dictionary<string, string> Specifications { get; init; } = [];
    public string Slug { get; init; } = null!;

    public ProductVariantDto(Money price, byte? discountPercentage, Money? priceBeforeDiscount, IReadOnlyCollection<ProductImage> images, string slug, IReadOnlyDictionary<string, string> specifications)
    {
        Slug = slug;
        Price = (double)price.Value;
        Price_Before_Discount = priceBeforeDiscount is null ? null : (double)priceBeforeDiscount.Value;

        Discount_Percentage = discountPercentage is null || discountPercentage == 0 ? null : discountPercentage;


        images.ToList().ForEach(productImage => Images.Add(new ImageDto(productImage)));
        Specifications = specifications.ToDictionary();


    }

}
