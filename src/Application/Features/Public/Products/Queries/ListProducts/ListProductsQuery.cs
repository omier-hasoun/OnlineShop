using Application.Common.ResponseModels;
using Application.Features.Public.Products.Dtos;

namespace Application.Features.Public.Products.Queries.ListProducts;

public sealed record ListProductsQuery : IRequest<Result<PaginatedList<ProductListItemDto>>>
{
    public required int PageNumber { get; init; }
    public required int PageSize { get; init; }
    public int? MaxPrice { get; init; }
    public string? SearchText { get; init; }
    public long? CategoryId { get; init; }
    public Guid? BrandId { get; init; }
}
