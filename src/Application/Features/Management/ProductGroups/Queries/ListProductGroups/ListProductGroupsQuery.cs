
using Application.Common.Dtos;
using Application.Features.Management.ProductGroups.Dtos;
using Domain.ProductGroups.Products;

namespace Application.Features.Management.ProductGroups.Queries.ListProductGroups;

public sealed record ListProductGroupsQuery : IRequest<Result<PaginatedList<ProductGroupListItemDto>>>
{
    public required int Page { get; init; } = 1;
    public required int Size { get; init; } = 25;

    public string? SearchText { get; init; }
    public long? CategoryId { get; init; }
    public Guid? BrandId { get; init; }

    public List<ProductGroupState>? Statuses { get; init; } = [ProductGroupState.Published];
}
