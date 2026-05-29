using Application.Common.Dtos;
using Application.Common.Extensions;
using Application.Features.Public.ProductsGroups.Dtos;
using Domain.ProductGroups.Products;
using Shared.Helpers;


namespace Application.Features.Public.ProductsGroups.Queries.ListProducts;

internal sealed class ListProductsQueryHandler(IAppDbContext context) : IRequestHandler<ListProductsQuery, Result<PaginatedList<ProductListItemDto>>>
{
    
    public async Task<Result<PaginatedList<ProductListItemDto>>> Handle(ListProductsQuery request, CancellationToken ct)
    {
        if(request.Size > 50)
        {
            return ApplicationErrors.Validation.PageSizeTooBig;
        }
        var query = context.ProductGroups
            .AsNoTracking()
            .GetPubishedProductGroups()
            .Where(g => g.FeaturedProductId != null)
            .ApplyBrandFilter(request.BrandId)
            .ApplyCategoryFilter(request.CategoryId)
            .ApplySearchTextFilter(request.SearchText);


        var prjoctionQuery = query.Select(g => new ProductListItemDto(
            g.Id,
            g.FeaturedProduct!.Id,
            g.Title,
            g.FeaturedProduct.Price,
            g.BrandName,
            g.AverageRating,
            g.FeaturedProduct.Images.FirstOrDefault(),
            g.FeaturedProduct.HasActiveDiscount,
            g.FeaturedProduct.PriceAfterDiscount,
            g.FeaturedProduct.DiscountPercentage,
            g.FeaturedProduct.StockPerWarehouse.Any(i => i.Quantity > 0)
        ));


        var result = await prjoctionQuery.ToPaginatedListAsync(
            request.Page,
            request.Size,
            ct);

        return result;

    }

    

}
