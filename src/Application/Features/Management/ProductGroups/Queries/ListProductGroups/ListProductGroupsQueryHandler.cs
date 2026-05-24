
using Application.Common.Dtos;
using Application.Common.Extensions;
using Application.Features.Management.ProductGroups.Dtos;

namespace Application.Features.Management.ProductGroups.Queries.ListProductGroups;

internal sealed class ListProductGroupsQueryHandler(IAppDbContext context, TimeProvider time) : IRequestHandler<ListProductGroupsQuery, Result<PaginatedList<ProductGroupListItemDto>>>
{
    public async Task<Result<PaginatedList<ProductGroupListItemDto>>> Handle(ListProductGroupsQuery request, CancellationToken ct)
    {
        var query = context.ProductGroups.AsNoTracking()
                            .ApplyStatusesFilter(request.Statuses)
                            .ApplySearchTextFilter(request.SearchText)
                            .ApplyBrandFilter(request.BrandId)
                            .ApplyCategoryFilter(request.CategoryId);
        
       var today = DateOnly.FromDateTime(time.GetUtcNow().Date);

        var projectionQuery = query.Join(context.Brands, g => g.BrandId, b => b.Id, (group, brand) => new { group, brand })
                            .Join(context.Categories, g => g.group.CategoryId, c => c.Id, (group, category) => new { group.group, group.brand, category})
                            .Select(x => new ProductGroupListItemDto(
                                    x.group.Id,
                                    x.group.Title,
                                    new ProductBrandDto(x.brand.Id, x.brand.Name),
                                    new ProductCategoryDto(x.category.Id, x.category.Name),
                                    x.group.AverageRating,
                                    x.group.Status,
                                    (byte)x.group.Products.Count
                            ));
            
        
        int resultTotalCount = await query.CountAsync(ct);

        var result = await projectionQuery.ToPaginatedListAsync(
            request.Page,
            request.Size, 
            resultTotalCount,
            ct
        );

        return result;

    }

}
