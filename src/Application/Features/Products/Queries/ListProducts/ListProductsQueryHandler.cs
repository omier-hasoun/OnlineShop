
using Application.Common.Extensions;
using Application.Common.ResponseModels;
using Application.Features.Products.Dtos;

namespace Application.Features.Products.Queries.ListProducts;

internal sealed class ListProductsQueryHandler(IAppDbContext context) : IRequestHandler<ListProductsQuery, Result<PaginatedList<ProductListItemDto>>>
{
    public async Task<Result<PaginatedList<ProductListItemDto>>> Handle(ListProductsQuery request, CancellationToken ct)
    {
        var totalCount = await context.Products.CountAsync(ct);

        var products = await context.Products.AsNoTracking()
                        .Skip(request.PageNumber - 1 * request.PageSize)
                        .Take(request.PageSize)
                        .Join(
                                context.ProductVariants,
                                p => p.Id,
                                v => v.ProductId,
                                (p, v) => new { p, v }
                        )
                        .Join(
                            context.Brands,
                            pv => pv.p.BrandId,
                            b => b.Id,
                            (pv, b) => new { pv, b }
                        )
                        .Select((selector) =>  new ProductListItemDto
                        (
                            ProductId: selector.pv.p.Id.Value,
                            Title: selector.pv.p.Title,
                            PriceNow: (double)selector.pv.v.PriceNow.Value,
                            Brand: selector.b.Name,
                            AverageRating: selector.pv.p.AverageRating.Value,
                            ImagePath: selector.pv.v.Images.First(x => x.SortOrder == 1)!.FilePath,
                            DiscountPercentage: selector.pv.v.DiscountPercentage,
                            OriginalPrice: (double)selector.pv.v.OriginalPrice.Value

                        )).ToListAsync(ct);

        var productsPage = products.ToPaginatedList(request.PageNumber, totalCount);

        return productsPage;
    }
}
