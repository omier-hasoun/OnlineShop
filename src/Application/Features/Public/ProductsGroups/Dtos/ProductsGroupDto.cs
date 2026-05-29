using Domain.ProductGroups.Products;
using Domain.ProductGroups.ValueObjects;

namespace Application.Features.Public.ProductsGroups.Dtos;

public sealed record ProductsGroupDto
{
    public long ProductGroupId { get; }
    public long ProductId { get; }

    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public IReadOnlyDictionary<string,string> Attributes { get; init; } = null!;
    public string Brand { get; init; } = null!;
    public string Category { get; init; } = null!;
    public float AverageRating { get; init; }

    public IReadOnlyList<ProductDto> Products { get; init; } = null!;

    public ProductsGroupDto(ProductGroupId productGroupId, ProductId? productId, string title, string description, IReadOnlyDictionary<string, string> attributes,
        string brand, string category, ProductAverageRating averageRating, IReadOnlyList<ProductDto> products)
    {
        ProductGroupId = productGroupId.Value;
        ProductId = productId!.Value.Value;
        Title = title;
        Description = description;
        Attributes = attributes;
        Brand = brand;
        Category = category;
        Products = products;
        AverageRating = averageRating.GetRoundedValue();

    }

}
