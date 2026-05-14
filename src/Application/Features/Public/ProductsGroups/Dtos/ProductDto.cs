using Application.Common.ResponseModels;
using Domain.Common.ValueObjects;
using Domain.ProductsGroups.Products;
using Domain.ProductsGroups.ValueObjects;

namespace Application.Features.Public.ProductsGroups.Dtos;

public sealed record ProductDto
{
    public long Id { get; }
    public double Price { get; }
    public double? PriceBeforeDiscount { get; }
    public byte? DiscountPercentage { get; }

    public List<ProductImageDto> Images { get; } = [];
    public Dictionary<string, string> Specifications { get; } = [];
    public string Slug { get; } = null!;

    public ProductDto(ProductId id, Money price, byte? discountPercentage, Money? priceBeforeDiscount, List<ProductImage> images, string slug, Dictionary<string, string> specifications)
    {
        Id = id.Value;
        Slug = slug;
        Price = (double)price.Value;
        PriceBeforeDiscount = priceBeforeDiscount is null ? null : (double)priceBeforeDiscount.Value;

        DiscountPercentage = discountPercentage ?? discountPercentage;

        images.ForEach(productImage => Images.Add(new ProductImageDto(productImage)));

        Specifications = specifications;


    }

}
