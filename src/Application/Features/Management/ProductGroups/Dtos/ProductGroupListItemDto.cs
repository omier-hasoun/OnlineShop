
using Domain.Common.ValueObjects;
using Domain.ProductGroups.ValueObjects;

namespace Application.Features.Management.ProductGroups.Dtos;

public sealed record ProductGroupListItemDto
{
    public string Id { get; init; }
    public string Title { get; init; } = null!;
    public ProductBrandDto Brand { get; init; } = null!;
    public ProductCategoryDto Category { get; init; } = null!;
    public string Status { get; init; }
    public float AverageRating { get; init; }
    public byte ProductsCount { get; set; }

    public ProductGroupListItemDto(ProductGroupId id, string title, ProductBrandDto brand, ProductCategoryDto category,
        ProductAverageRating averageRating, ProductGroupState productsGroupStatus, byte productsCount)
    {
        Id = id.ToString();
        Title = title;
        Brand = brand;
        ProductsCount = productsCount;
        Status = productsGroupStatus.ToString();
        AverageRating = averageRating.Value;
        Category = category;
    }
}
