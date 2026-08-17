using Domain.ProductGroups.Products;
using Domain.ProductGroups.ValueObjects;

namespace Application.Features.Public.Products.Dtos;

public sealed record ProductGroupDto
{
    public long ProductGroupId { get; }
    public long FeaturedProductId { get; }

    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public IReadOnlyDictionary<string, string> Attributes { get; init; } = null!;
    public string Brand { get; init; } = null!;
    public string Category { get; init; } = null!;
    public float AverageRating { get; init; }

    public IEnumerable<ProductDto> Products { get; init; } = null!;

    public ProductGroupDto(ProductGroupId productGroupId, ProductId? featuredProductId, string title, string description, IReadOnlyDictionary<string, string> attributes,
        string brand, string category, ProductAverageRating averageRating, IEnumerable<ProductDto> products)
    {
        ProductGroupId = productGroupId.Value;
        FeaturedProductId = featuredProductId!.Value.Value;
        Title = title;
        Description = description;
        Attributes = attributes;
        Brand = brand;
        Category = category;
        Products = products;
        AverageRating = averageRating.GetRoundedValue();

    }

}
