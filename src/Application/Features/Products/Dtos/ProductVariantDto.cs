
using Domain.Common.ValueObjects;
using Domain.Products.ValueObjects;

namespace Application.Features.Products.Dtos;

public sealed record ProductVariantDto
{
    public double Price { get; init; }
    public double? PriceBeforeDiscount { get; init; }
    public byte? DiscountPercentage { get; init; }

    public List<ImageDto> Images { get; init; } = [];
    public Dictionary<string, string> Specifications { get; init; } = [];
    public string Slug { get; init; } = null!;

    public ProductVariantDto(Money price, byte? discountPercentage, Money? priceBeforeDiscount, IReadOnlyCollection<ProductImage> images, string slug, IReadOnlyDictionary<string, string> specifications)
    {
        Slug = slug;
        Price = (double)price.Value;
        PriceBeforeDiscount = priceBeforeDiscount is null ? null : (double)priceBeforeDiscount.Value;

        DiscountPercentage = discountPercentage is null || discountPercentage == 0 ? null : discountPercentage;


        images.ToList().ForEach(productImage => Images.Add(new ImageDto(productImage)));
        Specifications = specifications.ToDictionary();


    }

}
