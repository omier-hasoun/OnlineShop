using Application.Common.Extensions;
using Application.Common.ResponseModels;
using Application.Features.Public.ProductsGroups.Dtos;
using Domain.Brands;
using Domain.Categories;
using Domain.Common.ValueObjects;
using Domain.ProductsGroups.Products;

namespace Application.Features.Public.ProductsGroups.Queries.ListProducts;

internal sealed class ListProductsQueryHandler(IAppDbContext context) : IRequestHandler<ListProductsQuery, Result<PaginatedList<ProductListItemDto>>>
{
    public async Task<Result<PaginatedList<ProductListItemDto>>> Handle(ListProductsQuery request, CancellationToken ct)
    {
        if(request.Size > 50)
        {
            return ApplicationErrors.Validation.PageSizeTooBig;
        }

        var cheapestProductQuery = context.ProductGroups.AsNoTracking()
            .Where(product => product.Status == ProductsGroupStatus.Published)
            .SelectMany(
                product => context.Products
                    .Where(variant => variant.ProductsGroupId == product.Id && variant.Status == ProductStatus.Published)
                    .OrderBy(v => v.Price)
                    .Take(1),
                (product, variant) => new { product, variant }
            );



        if (request.MaxPrice != null && request.MaxPrice > 0)
        {
            var maxPrice = Money.From((int)request.MaxPrice).Value;

            cheapestProductQuery = cheapestProductQuery.Where(x => x.variant.Price <= maxPrice);
        }

        var queryWithBrands = cheapestProductQuery.Join(
            context.Brands,
            pv => pv.product.BrandId, 
            brand => brand.Id,
            (pv, brand) => new { pv.product, pv.variant, brand }
        ).Where(x => x.brand.IsActive);



        if (request.BrandId.HasValue)
        {
            var brandId =request.ParsedBrandId;
            queryWithBrands = queryWithBrands.Where(x => x.product.BrandId == brandId);
        }

        if (request.CategoryId.HasValue)
        {
            var categoryId = request.ParsedCategoryId;

            queryWithBrands = queryWithBrands.Where(x => x.product.CategoryId == categoryId);
        }

        if (request.SearchText != null && request.SearchText.Length <= 100)
        {
            queryWithBrands = queryWithBrands.Where(x => x.product.Title.ToLower().Contains(request.SearchText));
        }

        int skip = ((request.Page - 1) * request.Size);

        var finalQuery = queryWithBrands
            .OrderBy(x => x.product.Id)
            .Skip(skip)
            .Take(request.Size)
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

        var productsPage = result.Select(x => x.dto).ToList().ToPaginatedList(request.Page, resultTotalCount);

        return productsPage;

    }

    

}
