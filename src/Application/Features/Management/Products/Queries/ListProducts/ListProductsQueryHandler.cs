
using Application.Common.Extensions;
using Application.Common.ResponseModels;
using Application.Features.Management.Products.Dtos;
using Domain.Brands;
using Domain.Categories;
using Domain.Common.ValueObjects;

namespace Application.Features.Management.Products.Queries.ListProducts;

internal sealed class ListProductsQueryHandler(IAppDbContext context) : IRequestHandler<ListProductsQuery, Result<PaginatedList<ProductListItemDto>>>
{
    public async Task<Result<PaginatedList<ProductListItemDto>>> Handle(ListProductsQuery query, CancellationToken ct)
    {
        if (query.Size > 50)
        {
            return ApplicationErrors.Validation.PageSizeTooBig;
        }

        var stateQuery = context.Products.AsNoTracking();

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
            stateQuery = stateQuery.Where(x => statuses.Contains(x.Status));

        var GetFirstVariantQuery = stateQuery.SelectMany(
                product => context.ProductVariants
                    .Where(variant => variant.ProductId == product.Id)
                    .OrderBy(v => v.Price)
                    .Take(1),
                (product, variant) => new { product, variant }
            ); 


        if(query.GetDiscountedProductsOnly)
        {
            GetFirstVariantQuery = GetFirstVariantQuery.
                SelectMany(product => context.ProductVariants.Where(variant => variant.DiscountPercentage != null), (product, variant) => new { product.product, product.variant });
        }




        if (query.MaxPrice != null && query.MaxPrice > 0)
        {
            var maxPrice = Money.From((int)query.MaxPrice).Value;

            GetFirstVariantQuery = GetFirstVariantQuery.Where(x => x.variant.Price <= maxPrice);
        }

        var queryWithBrands = GetFirstVariantQuery.Join(
            context.Brands,
            pv => pv.product.BrandId,
            brand => brand.Id,
            (pv, brand) => new { pv.product, pv.variant, brand }
        ).Where(x => x.brand.IsActive);



        if (query.BrandId != null)
        {
            var brandId = new BrandId((Guid)query.BrandId);
            queryWithBrands = queryWithBrands.Where(x => x.product.BrandId == brandId);
        }

        if (query.CategoryId != null)
        {
            var categoryId = new CategoryId((long)query.CategoryId);

            queryWithBrands = queryWithBrands.Where(x => x.product.CategoryId == categoryId);
        }

        if (query.SearchText != null && query.SearchText.Length <= 100)
        {
            queryWithBrands = queryWithBrands.Where(x => x.product.Title.ToLower().Contains(query.SearchText));
        }

        int skip = ((query.Page - 1) * query.Size);

        var finalQuery = queryWithBrands
            .OrderBy(x => x.product.Id)
            .Skip(skip)
            .Take(query.Size)
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
                    x.variant.DiscountPercentage,
                    x.product.Status
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
