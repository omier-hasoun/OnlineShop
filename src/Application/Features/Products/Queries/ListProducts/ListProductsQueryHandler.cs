
using Application.Common.Extensions;
using Application.Common.ResponseModels;
using Application.Features.Products.Dtos;
using Domain.Brands;
using Domain.Categories;
using Domain.Common.ValueObjects;

namespace Application.Features.Products.Queries.ListProducts;

internal sealed class ListProductsQueryHandler(IAppDbContext context) : IRequestHandler<ListProductsQuery, Result<PaginatedList<ProductListItemDto>>>
{
    public async Task<Result<PaginatedList<ProductListItemDto>>> Handle(ListProductsQuery request, CancellationToken ct)
    {
        if(request.PageSize > 50)
        {
            return ApplicationErrors.Validation.PageSizeTooBig;
        }

        var cheapestVariantsQuery = context.Products.AsNoTracking()
            .Where(product => product.Status == ProductStatus.Active)
            .SelectMany(
                product => context.ProductVariants
                    .Where(variant => variant.ProductId == product.Id && variant.Status == ProductStatus.Active)
                    .OrderBy(v => v.Price)
                    .Take(1),
                (product, variant) => new { product, variant }
            );



        if (request.MaxPrice != null && request.MaxPrice > 0)
        {
            var maxPrice = Money.From((int)request.MaxPrice).Value;

            cheapestVariantsQuery = cheapestVariantsQuery.Where(x => x.variant.Price <= maxPrice);
        }

        var queryWithBrands = cheapestVariantsQuery.Join(
            context.Brands,
            pv => pv.product.BrandId, 
            brand => brand.Id,
            (pv, brand) => new { pv.product, pv.variant, brand }
        ).Where(x => x.brand.IsActive);



        if (request.BrandId != null)
        {
            var brandId = new BrandId((Guid)request.BrandId);
            queryWithBrands = queryWithBrands.Where(x => x.product.BrandId == brandId);
        }

        if (request.CategoryId != null)
        {
            var categoryId = new CategoryId((long)request.CategoryId);

            queryWithBrands = queryWithBrands.Where(x => x.product.CategoryId == categoryId);
        }

        if (request.SearchText != null && request.SearchText.Length <= 100)
        {
            queryWithBrands = queryWithBrands.Where(x => x.product.Title.ToLower().Contains(request.SearchText));
        }

        int skip = ((request.PageNumber - 1) * request.PageSize);

        var finalQuery = queryWithBrands
            .OrderBy(x => x.product.Id)
            .Skip(skip)
            .Take(request.PageSize)
            .Select(x => new
            {
                dto = new ProductListItemDto(
                    x.product.Id,
                    x.product.Title,
                    x.variant.PriceBeforeDiscount,
                    x.brand.Name,
                    x.product.AverageRating,
                    x.variant.Price,
                    x.variant.Images.OrderBy(img => img.SortOrder).FirstOrDefault()!,
                    x.variant.DiscountPercentage

                    ),

                TotalCount = queryWithBrands.Count()
            }
            );

        var result = await finalQuery.ToListAsync(ct);

        int resultTotalCount = result.Select(x => x.TotalCount).FirstOrDefault();

        var productsPage = result.Select(x => x.dto).ToList().ToPaginatedList(request.PageNumber, resultTotalCount);

        return productsPage;

    }

    

}
