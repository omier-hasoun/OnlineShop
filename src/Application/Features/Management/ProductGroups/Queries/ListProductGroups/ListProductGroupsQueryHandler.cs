
using Application.Common.Dtos;
using Application.Common.Extensions;
using Application.Features.Management.ProductGroups.Dtos;

namespace Application.Features.Management.ProductGroups.Queries.ListProductGroups;

internal sealed class ListProductGroupsQueryHandler(IAppDbContext context) : IRequestHandler<ListProductGroupsQuery, Result<PaginatedList<ProductGroupListItem>>>
{
    public async Task<Result<PaginatedList<ProductGroupListItem>>> Handle(ListProductGroupsQuery request, CancellationToken ct)
    {
        if (request.Size > 50)
        {
            return ApplicationErrors.Validation.PageSizeTooBig;
        }
        var query = context.ProductGroups.AsNoTracking()
                            .ApplyStatusesFilter(request.Statuses)
                            .ApplySearchTextFilter(request.SearchText)
                            .ApplyBrandFilter(request.BrandId)
                            .ApplyCategoryFilter(request.CategoryId);
        
       var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var projectionQuery = query.Join(context.Brands, g => g.BrandId, b => b.Id, (group, brand) => new { group, brand })
                            .Join(context.Categories, g => g.group.CategoryId, c => c.Id, (group, category) => new { group.group, group.brand, category})
                            .Select(x => new ProductGroupListItem(
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
