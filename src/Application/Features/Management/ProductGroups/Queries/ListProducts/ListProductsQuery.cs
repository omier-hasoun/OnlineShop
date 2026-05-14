
using Application.Common.ResponseModels;
using Application.Features.Management.ProductGroups.Dtos;

namespace Application.Features.Management.ProductGroups.Queries.ListProducts;

public sealed record ListProductsQuery : IRequest<Result<PaginatedList<ProductListItemDto>>>
{
    public required int Page { get; init; }
    public required int Size { get; init; }
    public int? MaxPrice { get; init; }
    public string? SearchText { get; init; }
    public long? CategoryId { get; init; }
    public Guid? BrandId { get; init; }
    public bool GetDiscountedProductsOnly { get; init; } = false;

    public bool GetPublishedProducts { get; init; } = true;
    public bool GetUnpublishedProducts { get; init; } = true;
    public bool GetArchivedProducts { get; init; } = true;
    public bool GetDraftProducts { get; init; } = true;


}
