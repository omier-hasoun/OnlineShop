using Domain.Common.ValueObjects;
using Domain.Products.ValueObjects;

namespace Application.Features.Products.Dtos;

public sealed record ProductDto
{
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public Dictionary<string,string> Attributes { get; init; } = null!;
    public string Brand { get; init; } = null!;
    public string Category { get; init; } = null!;
    public float AverageRating { get; init; }

    public ICollection<ProductVariantDto> Variants { get; init; } = null!;

    public ProductDto(string title, string description, IReadOnlyDictionary<string, string> attributes, string brand, string category, ProductAverageRating averageRating,
        List<ProductVariantDto> variants)
    {
        Title = title;
        Description = description;
        Attributes = attributes.ToDictionary();
        Brand = brand;
        Category = category;
        Variants = variants;
        AverageRating = averageRating.Value;

    }

}
