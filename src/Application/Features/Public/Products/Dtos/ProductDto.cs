using Application.Common.Dtos;
using Domain.Common.ValueObjects;
using Domain.ProductGroups.Products;
using Domain.ProductGroups.ValueObjects;

namespace Application.Features.Public.Products.Dtos;

public sealed record ProductDto
{
    public long Id { get; }
    public double Price { get; }
    public bool HasDiscount { get; }
    public double? DiscountPrice { get; }
    public byte? DiscountPercentage { get; }

    public bool InStock { get; }
    public string Slug { get; } = null!;
    public List<ProductImageDto> Images { get; } = [];
    public IReadOnlyDictionary<string, string> Specifications { get; } = new Dictionary<string, string>();

    public ProductDto(ProductId id, Money price, bool hasDiscount, byte? discountPercentage, Money? discountPrice,
        IReadOnlyCollection<ProductImage> images, string slug, IReadOnlyDictionary<string, string> specifications, bool inStock)
    {
        Id = id.Value;
        Price = (double)price.Value;
        Slug = slug;
        HasDiscount = hasDiscount;
        DiscountPrice = hasDiscount is false ? null : (double)discountPrice!.Value;
        DiscountPercentage = hasDiscount is false ? null : discountPercentage;

        foreach (var productImage in images)
        {
            Images.Add(new ProductImageDto(productImage));
        }

        Specifications = specifications;
        InStock = inStock;
    }

}
