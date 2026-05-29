
using Application.Common.Dtos;
using Application.Common.Extensions;
using Application.Features.Management.ProductGroups.Dtos;

namespace Application.Features.Management.ProductGroups.Queries.ListProductGroups;

internal sealed class ListProductGroupsQueryHandler(IAppDbContext context) : IRequestHandler<ListProductGroupsQuery, Result<PaginatedList<ProductGroupListItemDto>>>
{
    public async Task<Result<PaginatedList<ProductGroupListItemDto>>> Handle(ListProductGroupsQuery request, CancellationToken ct)
    {
        var query = context.ProductGroups.AsNoTracking()
                            .ApplyStatusesFilter(request.Statuses)
                            .ApplySearchTextFilter(request.SearchText)
                            .ApplyBrandFilter(request.BrandId)
                            .ApplyCategoryFilter(request.CategoryId);

        var projectionQuery = query.Select(x => new ProductGroupListItemDto(
                                    x.Id,
                                    x.Title,
                                    x.BrandName,
                                    x.CategoryName,
                                    x.AverageRating,
                                    x.Status,
                                    x.Products.Count));


        var result = await projectionQuery.ToPaginatedListAsync(
            request.Page,
            request.Size,
            ct
        );

        return result;

    }

}
