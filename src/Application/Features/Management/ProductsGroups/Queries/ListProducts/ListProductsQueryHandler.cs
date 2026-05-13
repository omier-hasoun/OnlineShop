
using Application.Common.Extensions;
using Application.Common.ResponseModels;
using Application.Features.Management.ProductsGroups.Dtos;
using Domain.Brands;
using Domain.Categories;
using Domain.Common.ValueObjects;

namespace Application.Features.Management.ProductsGroups.Queries.ListProducts;

internal sealed class ListProductsQueryHandler(IAppDbContext context) : IRequestHandler<ListProductsQuery, Result<PaginatedList<ProductListItemDto>>>
{
    public async Task<Result<PaginatedList<ProductListItemDto>>> Handle(ListProductsQuery query, CancellationToken ct)
    {
        if (query.Size > 50)
        {
            return ApplicationErrors.Validation.PageSizeTooBig;
        }

        var containsStatusQuery = context.ProductGroups.AsNoTracking();

        var statuses = new List<ProductsGroupStatus>();

        if (query.GetPublishedProducts)
            statuses.Add(ProductsGroupStatus.Published);

        if (query.GetUnpublishedProducts)
            statuses.Add(ProductsGroupStatus.Unpublished);

        if (query.GetDraftProducts)
            statuses.Add(ProductsGroupStatus.Draft);

        if (query.GetArchivedProducts)
            statuses.Add(ProductsGroupStatus.Archived);

        if (statuses.Count > 0)
            containsStatusQuery = containsStatusQuery.Where(x => statuses.Contains(x.Status));

        var GetFirstProductQuery = containsStatusQuery.SelectMany(
                productGroup => context.Products
                    .Where(product => product.ProductsGroupId == productGroup.Id)
                    .OrderBy(v => v.Price)
                    .Take(1),
                (productsGroup, products) => new { productsGroup, products }
            ); 


        if(query.GetDiscountedProductsOnly)
        {
            GetFirstProductQuery = GetFirstProductQuery.
                SelectMany(productGroup => context.Products.
                Where(products => products.DiscountPercentage != null),
                (productGroup, products) => new { productGroup.productsGroup, productGroup.products });
        }


        if (query.MaxPrice != null && query.MaxPrice > 0)
        {
            var maxPrice = Money.From((int)query.MaxPrice).Value;

            GetFirstProductQuery = GetFirstProductQuery.Where(x => x.products.Price <= maxPrice);
        }

        var queryWithBrands = GetFirstProductQuery.Join(
                                    context.Brands,
                                    pv => pv.productsGroup.BrandId,
                                    brand => brand.Id,
                                    (pv, brand) => new { pv.productsGroup, pv.products, brand }
                                ).Where(x => x.brand.IsActive);



        if (query.BrandId != null)
        {
            var brandId = new BrandId((Guid)query.BrandId);
            queryWithBrands = queryWithBrands.Where(x => x.productsGroup.BrandId == brandId);
        }

        if (query.CategoryId != null)
        {
            var categoryId = new CategoryId((long)query.CategoryId);

            queryWithBrands = queryWithBrands.Where(x => x.productsGroup.CategoryId == categoryId);
        }

        if (query.SearchText != null && query.SearchText.Length <= 100)
        {
            queryWithBrands = queryWithBrands.Where(x => x.productsGroup.Title.ToLower().Contains(query.SearchText));
        }

        int skip = ((query.Page - 1) * query.Size);

        var finalQuery = queryWithBrands
            .OrderBy(x => x.productsGroup.Id)
            .Skip(skip)
            .Take(query.Size)
            .Select(x => new
            {
                dto = new ProductListItemDto(
                    x.productsGroup.Id,
                    x.productsGroup.Title,
                    x.products.PriceBeforeDiscount,
                    x.brand.Name,
                    x.productsGroup.AverageRating,
                    x.products.Price,
                    x.products.Images.OrderBy(img => img.SortOrder).FirstOrDefault()!,
                    x.products.DiscountPercentage,
                    x.productsGroup.Status
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
