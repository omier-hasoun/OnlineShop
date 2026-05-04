
using Application.Common.Extensions;
using Application.Common.ResponseModels;
using Application.Features.Products.Dtos;


namespace Application.Features.Products.Queries.ListProducts;

internal sealed class ListProductsQueryHandler(IAppDbContext context) : IRequestHandler<ListProductsQuery, Result<PaginatedList<ProductListItemDto>>>
{
    public async Task<Result<PaginatedList<ProductListItemDto>>> Handle(ListProductsQuery request, CancellationToken ct)
    {
        if(request.PageSize > 50 || request.PageNumber > 1000)// just making sure no one is insane enough to request page 1000
        {
            return ApplicationErrors.Validation.PageSizeTooBig;
        }

        int skip = ((request.PageNumber - 1) * request.PageSize);
        int totalProductsCount = await context.Products.Where(p => p.Status == ProductStatus.Active).CountAsync(ct);

        var query = context.Products
            .AsNoTracking()
            .Where(p => p.Status == ProductStatus.Active)
            .SelectMany(
                p => context.ProductVariants
                    .Where(v => v.ProductId == p.Id && v.Status == ProductStatus.Active)
                    .OrderBy(v => v.PriceNow)
                    .Take(1),
                (p, v) => new { p, v }
            )
            .Join(context.Brands,
                pv => pv.p.BrandId,
                b => b.Id,
                (pv, b) => new { pv.p, pv.v, b })
            .Where(x => x.b.IsActive)
            .OrderBy(x => x.p.Id)
            .Skip(skip)
            .Take(request.PageSize)
            .Select(m => new ProductListItemDto
            {
                Id = m.p.Id,
                Title = m.p.Title,
                AverageRating = m.p.AverageRating,
                Brand = m.b.Name,
                Image = m.v.Images.First(x => x.SortOrder == 1),
                OriginalPrice = m.v.OriginalPrice,
                PriceNow = m.v.PriceNow,
                DiscountPercentage = m.v.DiscountPercentage
            });


        var productsPage = await query.ToListAsync(ct);
        return productsPage.ToPaginatedList(request.PageNumber, totalProductsCount) ;
        
    }

}
