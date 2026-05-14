

using Application.Common.ResponseModels;
using Domain.Common.ValueObjects;
using Domain.ProductsGroups.Products;
using Domain.ProductsGroups.ValueObjects;

namespace Application.Features.Management.ProductsGroups.Dtos;

public sealed record ProductDto
{
    public string Id { get; init; }
    public double Price { get; init; }
    public double? PriceBeforeDiscount { get; init; }
    public byte? DiscountPercentage { get; init; }

    public List<ProductImageDto> Images { get; init; } = [];
    public Dictionary<string, string> Specifications { get; init; } = [];
    public string Slug { get; init; } = null!;

    public ProductDto(ProductId id, Money price, byte? discountPercentage, Money? priceBeforeDiscount, List<ProductImage> images, string slug, Dictionary<string, string> specifications)
    {
        Id = id.ToString();
        Slug = slug;
        Price = (double)price.Value;
        PriceBeforeDiscount = priceBeforeDiscount is null ? null : (double)priceBeforeDiscount.Value;

        DiscountPercentage = discountPercentage ?? discountPercentage;

        images.ForEach(productImage => Images.Add(new ProductImageDto(productImage)));

        Specifications = specifications;


    }
}
