using Domain.ProductsGroups.ValueObjects;

namespace Application.Features.Public.ProductsGroups.Dtos;

public sealed record ProductsGroupDto
{
    public long Id { get; }
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public Dictionary<string,string> Attributes { get; init; } = null!;
    public string Brand { get; init; } = null!;
    public string Category { get; init; } = null!;
    public float AverageRating { get; init; }

    public List<ProductDto> Variants { get; init; } = null!;

    public ProductsGroupDto(ProductsGroupId id, string title, string description, Dictionary<string, string> attributes, string brand, string category, ProductAverageRating averageRating,
        List<ProductDto> variants)
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
