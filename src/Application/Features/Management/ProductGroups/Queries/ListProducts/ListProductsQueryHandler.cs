
using Application.Common.Extensions;
using Application.Common.ResponseModels;
using Application.Features.Management.ProductGroups.Dtos;
using Domain.Brands;
using Domain.Categories;
using Domain.Common.ValueObjects;

namespace Application.Features.Management.ProductGroups.Queries.ListProducts;

internal sealed class ListProductsQueryHandler(IAppDbContext context) : IRequestHandler<ListProductsQuery, Result<PaginatedList<ProductListItemDto>>>
{
    public async Task<Result<PaginatedList<ProductListItemDto>>> Handle(ListProductsQuery query, CancellationToken ct)
    {
        if (query.Size > 50)
        {
            return ApplicationErrors.Validation.PageSizeTooBig;
        }

        var containsStatusQuery = context.ProductGroups.AsNoTracking();

        var statuses = new List<ProductStatus>();

        if (query.GetPublishedProducts)
            statuses.Add(ProductStatus.Published);

        if (query.GetUnpublishedProducts)
            statuses.Add(ProductStatus.Unpublished);

        if (query.GetDraftProducts)
            statuses.Add(ProductStatus.Draft);

        if (query.GetArchivedProducts)
            statuses.Add(ProductStatus.Archived);

        if (statuses.Count > 0)
            containsStatusQuery = containsStatusQuery.Where(x => statuses.Contains(x.Status));

        var GetFirstProductQuery = containsStatusQuery.SelectMany(
                productGroup => context.Products
                    .Where(product => product.ProductGroupId == productGroup.Id)
                    .OrderBy(v => v.Price)
                    .Take(1),
                (productGroup, variant) => new { productGroup, variant }
            ); 


        if(query.GetDiscountedProductsOnly)
        {
            GetFirstProductQuery = GetFirstProductQuery.
                SelectMany(productGroup => context.Products.Where(variant => variant.DiscountPercentage != null), (productGroup, variant) => new { productGroup.productGroup, productGroup.variant });
        }




        if (query.MaxPrice != null && query.MaxPrice > 0)
        {
            var maxPrice = Money.From((int)query.MaxPrice).Value;

            GetFirstProductQuery = GetFirstProductQuery.Where(x => x.variant.Price <= maxPrice);
        }

        var queryWithBrands = GetFirstProductQuery.Join(
            context.Brands,
            pv => pv.productGroup.BrandId,
            brand => brand.Id,
            (pv, brand) => new { pv.productGroup, pv.variant, brand }
        ).Where(x => x.brand.IsActive);



        if (query.BrandId != null)
        {
            var brandId = new BrandId((Guid)query.BrandId);
            queryWithBrands = queryWithBrands.Where(x => x.productGroup.BrandId == brandId);
        }

        if (query.CategoryId != null)
        {
            var categoryId = new CategoryId((long)query.CategoryId);

            queryWithBrands = queryWithBrands.Where(x => x.productGroup.CategoryId == categoryId);
        }

        if (query.SearchText != null && query.SearchText.Length <= 100)
        {
            queryWithBrands = queryWithBrands.Where(x => x.productGroup.Title.ToLower().Contains(query.SearchText));
        }

        int skip = ((query.Page - 1) * query.Size);

        var finalQuery = queryWithBrands
            .OrderBy(x => x.productGroup.Id)
            .Skip(skip)
            .Take(query.Size)
            .Select(x => new
            {
                dto = new ProductListItemDto(
                    x.productGroup.Id,
                    x.productGroup.Title,
                    x.variant.PriceBeforeDiscount,
                    x.brand.Name,
                    x.productGroup.AverageRating,
                    x.variant.Price,
                    x.variant.Images.OrderBy(img => img.SortOrder).FirstOrDefault()!,
                    x.variant.DiscountPercentage,
                    x.productGroup.Status
                    ),

                TotalCount = queryWithBrands.Count()
            }
            );

        var result = await finalQuery.ToListAsync(ct);

        int resultTotalCount = result.Select(x => x.TotalCount).FirstOrDefault();

        var productsPage = result.Select(x => x.dto).ToList().ToPaginatedList(query.Page, resultTotalCount);

        return productsPage;

    }

}
