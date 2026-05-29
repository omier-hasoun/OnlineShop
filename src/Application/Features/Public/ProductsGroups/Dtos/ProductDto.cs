using Application.Common.Dtos;
using Domain.Common.ValueObjects;
using Domain.ProductGroups.Products;
using Domain.ProductGroups.ValueObjects;

namespace Application.Features.Public.ProductsGroups.Dtos;

public sealed record ProductDto
{
    public long Id { get; }
    public double Price { get; }
    public bool HasActiveDiscount { get; }
    public double? PriceAfterDiscount { get; }
    public byte? DiscountPercentage { get; }

    public bool IsAvailable { get; }
    public string Slug { get; } = null!;
    public List<ProductImageDto> Images { get; } = [];
    public IReadOnlyDictionary<string, string> Specifications { get; } = new Dictionary<string,string>();

    public ProductDto(ProductId id, Money price, bool hasActiveDiscount, byte? discountPercentage, Money? priceAfterDiscount, 
        IReadOnlyCollection<ProductImage> images, string slug, IReadOnlyDictionary<string, string> specifications, bool isAvailable)
    {
        Id = id.Value;
        Price = (double)price.Value;
        Slug = slug;
        HasActiveDiscount = hasActiveDiscount;
        PriceAfterDiscount = hasActiveDiscount is false ? null : (double)priceAfterDiscount!.Value;
        DiscountPercentage = hasActiveDiscount is false ? null : discountPercentage;

        foreach (var productImage in images)
        {
            Images.Add(new ProductImageDto(productImage));
        }

        Specifications = specifications;
        IsAvailable = isAvailable;
    }

}
