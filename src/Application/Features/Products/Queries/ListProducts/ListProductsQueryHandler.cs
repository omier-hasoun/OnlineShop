
using Application.Common.Extensions;
using Application.Common.ResponseModels;
using Application.Features.Products.Dtos;

namespace Application.Features.Products.Queries.ListProducts;

internal sealed class ListProductsQueryHandler(IAppDbContext context) : IRequestHandler<ListProductsQuery, Result<PaginatedList<ProductListItemDto>>>
{
    public async Task<Result<PaginatedList<ProductListItemDto>>> Handle(ListProductsQuery request, CancellationToken ct)
    {
        if(request.PageSize > 50)
        {
            return ApplicationErrors.Validation.PageSizeTooBig;
        }

        int skip = ((request.PageNumber - 1) * request.PageSize);


        var cheapestVariantsQuery = context.Products.AsNoTracking()
            .Where(product => product.Status == ProductStatus.Active)
            .SelectMany(
                product => context.ProductVariants
                    .Where(variant => variant.ProductId == product.Id && variant.Status == ProductStatus.Active)
                    .OrderBy(v => v.PriceNow)
                    .Take(1),
                (product, variant) => new { product, variant }
            );

        if (request.MaxPrice != null)
        {
            cheapestVariantsQuery = cheapestVariantsQuery.Where(x => x.variant.PriceNow <= request.MaxPrice);
        }

        var queryWithBrands = cheapestVariantsQuery.Join(
            context.Brands,
            pv => pv.product.BrandId, 
            brand => brand.Id,
            (pv, brand) => new { pv.product, pv.variant, brand }
        ).Where(x => x.brand.IsActive);



        if (request.BrandId != null)
        {
            queryWithBrands = queryWithBrands.Where(x => x.product.BrandId == request.BrandId);
        }

        if (request.CategoryId != null)
        {
            queryWithBrands = queryWithBrands.Where(x => x.product.CategoryId == request.CategoryId);
        }

        if (!string.IsNullOrEmpty(request.SearchText))
        {
            queryWithBrands = queryWithBrands.Where(x => x.product.Title.ToLower().Contains(request.SearchText));
        }

        var finalQuery = queryWithBrands
            .OrderBy(x => x.product.Id)
            .Skip(skip)
            .Take(request.PageSize)
            .Select(x => new
            {
                dto = new ProductListItemDto
                {
                    Id = x.product.Id,
                    Title = x.product.Title,
                    AverageRating = x.product.AverageRating,
                    Brand = x.brand.Name,
                    Image = x.variant.Images.OrderBy(img => img.SortOrder).FirstOrDefault()!,
                    OriginalPrice = x.variant.OriginalPrice,
                    PriceNow = x.variant.PriceNow,
                    DiscountPercentage = x.variant.DiscountPercentage
                },
                TotalCount = queryWithBrands.Count()
            }
            );

        var result = await finalQuery.ToListAsync(ct);

        int resultTotalCount = result.Select(x => x.TotalCount).FirstOrDefault();

        var productsPage = result.Select(x => x.dto).ToList().ToPaginatedList(request.PageNumber, resultTotalCount);

        return productsPage;

    }

    

}
