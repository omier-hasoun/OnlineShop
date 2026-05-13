using Domain.ProductGroups.ValueObjects;

namespace Application.Features.Public.Products.Dtos;

public sealed record ProductDto
{
    public long Id { get; }
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public Dictionary<string,string> Attributes { get; init; } = null!;
    public string Brand { get; init; } = null!;
    public string Category { get; init; } = null!;
    public float AverageRating { get; init; }

    public List<ProductVariantDto> Variants { get; init; } = null!;

    public ProductDto(ProductGroupId id, string title, string description, Dictionary<string, string> attributes, string brand, string category, ProductAverageRating averageRating,
        List<ProductVariantDto> variants)
    {
        Id = id.Value;
        Title = title;
        Description = description;
        Attributes = attributes;
        Brand = brand;
        Category = category;
        Variants = variants;
        AverageRating = averageRating.Value;

    }

}
