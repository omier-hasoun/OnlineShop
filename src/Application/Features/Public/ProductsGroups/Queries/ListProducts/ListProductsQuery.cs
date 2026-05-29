using Application.Common.Dtos;
using Application.Features.Public.ProductsGroups.Dtos;
using Domain.Brands;
using Domain.Categories;

namespace Application.Features.Public.ProductsGroups.Queries.ListProducts;

public sealed record ListProductsQuery : IRequest<Result<PaginatedList<ProductListItemDto>>>
{
    public int Page { get; init; } = 1;
    public int Size { get; init; } = 30;

    public Guid? BrandId { get; init; }
    public long? CategoryId { get; init; }

    public int? MaxPrice { get; init; } 
    public string? SearchText { get; init; }

    public bool DiscountedProductsOnly { get; init; } = false;

    internal BrandId? ParsedBrandId =>
        BrandId.HasValue ? new BrandId(BrandId.Value) : null;

    internal CategoryId? ParsedCategoryId =>
        CategoryId.HasValue ? new CategoryId(CategoryId.Value) : null;
}
