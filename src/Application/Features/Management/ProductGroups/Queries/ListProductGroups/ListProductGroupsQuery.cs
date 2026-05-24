
using Application.Common.Dtos;
using Application.Features.Management.ProductGroups.Dtos;

namespace Application.Features.Management.ProductGroups.Queries.ListProductGroups;

public sealed record ListProductGroupsQuery : IRequest<Result<PaginatedList<ProductGroupListItem>>>
{
    public required int Page { get; init; }
    public required int Size { get; init; }

    public string? SearchText { get; init; }
    public long? CategoryId { get; init; }
    public Guid? BrandId { get; init; }
    
    public List<ProductGroupState>? Statuses { get; init; }
}
