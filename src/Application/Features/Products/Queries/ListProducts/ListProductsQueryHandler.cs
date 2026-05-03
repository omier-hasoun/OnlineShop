
using Application.Common.Extensions;
using Application.Common.ResponseModels;
using Application.Features.Products.Dtos;
using Domain.Products.ValueObjects;
using Domain.Products.ProductVariants;
namespace Application.Features.Products.Queries.ListProducts;

internal sealed class ListProductsQueryHandler(IAppDbContext context) : IRequestHandler<ListProductsQuery, Result<PaginatedList<ProductListItemViewDto>>>
{
    public async Task<Result<PaginatedList<ProductListItemViewDto>>> Handle(ListProductsQuery request, CancellationToken ct)
    {
        if(request.PageSize > 50 || request.PageNumber > 1000)// just making sure no one is insane enough to request page 1000
        {
            return ApplicationErrors.Validation.PageSizeTooBig;
        }

        int skip = ((request.PageNumber - 1) * request.PageSize);
        int totalProductsCount = await context.Products.Where(p => p.Status == ProductStatus.Active).CountAsync(ct);
        //        var products = await context.Products.FromSqlInterpolated(
        //$@"
        //DECLARE @skip int
        //DECLARE @take smallInt
        //set @skip = {skip}
        //set @take = {request.PageSize}

        //SELECT p.Id,
        //       p.AverageRating,
        //       p.Title,
        //       b.Name as Brand,
        //       v.DiscountPercentage,
        //       v.OriginalPrice,
        //       v.Images,
        //       v.PriceNow
        //FROM Products p
        //JOIN Brands b ON p.BrandId = b.Id
        //CROSS APPLY (
        //    SELECT TOP 1
        //           v.DiscountPercentage,
        //           v.OriginalPrice,
        //           v.PriceNow,
        //		   v.Images
        //    FROM ProductVariants v
        //    WHERE v.ProductId = p.Id
        //    ORDER BY v.PriceNow ASC
        //) v
        //where Status = '{ProductStatus.Active}'
        //OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY").ToListAsync(ct);


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
            .Select(m => new ProductListItemViewDto
            {
                Id = m.p.Id.Value,
                Title = m.p.Title,
                AverageRating = m.p.AverageRating.Value,
                Brand = m.b.Name,
                Images = m.v.Images.ToList(),
                OriginalPrice = m.v.OriginalPrice.Value,
                PriceNow = m.v.PriceNow.Value,
                DiscountPercentage = m.v.DiscountPercentage
            });


        var productsPage = await query.ToListAsync(ct);
        return productsPage.ToPaginatedList(request.PageNumber, totalProductsCount);
        
    }

}
