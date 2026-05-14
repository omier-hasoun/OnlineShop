using Domain.Brands;
using Domain.Categories;
using Domain.ProductsGroups.ValueObjects;

namespace Application.Features.Management.ProductGroups.Dtos;

public sealed record ProductGroupDto
{
    public string Id { get; }
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;

    public float AverageRating { get; init; }
    public ProductCategoryDto Category { get; init; } = null!;
    public ProductBrandDto Brand { get; init; }
    public Dictionary<string, string> Attributes { get; init; } = null!;
    public List<ProductGroupListProductsDto> Products { get; init; } = null!;


    public ProductGroupDto(ProductsGroupId id, string title, string description, Dictionary<string, string> attributes,
        BrandId brandId, string brandName, CategoryId categoryId, string categoryName, ProductAverageRating averageRating,
        List<ProductGroupListProductsDto> products)
    {
        Id = id.ToString();
        Title = title;
        Description = description;
        Attributes = attributes;
        Brand = new(brandId, brandName);
        Category = new ProductCategoryDto(categoryId, categoryName);
        Products = products;
        AverageRating = averageRating.Value;

    }
}
