
using Application.Features.Management.Brands.Dtos;
using Application.Features.Management.Categories.Dtos;
using Domain.Brands;
using Domain.Categories;
using Domain.ProductsGroups.ValueObjects;

namespace Application.Features.Management.ProductGroups.Dtos;

public sealed record ProductsGroupDto
{
    public string Id { get; }
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;

    public float AverageRating { get; init; }
    public CategoryBasicDto Category { get; init; } = null!;
    public BrandBasicDto Brand { get; init; }
    public Dictionary<string, string> Attributes { get; init; } = null!;
    public List<ProductDto> Products { get; init; } = null!;


    public ProductsGroupDto(ProductsGroupId id, string title, string description, Dictionary<string, string> attributes,
        BrandId brandId, string brandName, CategoryId categoryId, string categoryName, ProductAverageRating averageRating,
        List<ProductDto> products)
    {
        Id = id.ToString();
        Title = title;
        Description = description;
        Attributes = attributes;
        Brand = new(brandId, brandName);
        Category = new CategoryBasicDto(categoryId, categoryName);
        Products = products;
        AverageRating = averageRating.Value;

    }
}
