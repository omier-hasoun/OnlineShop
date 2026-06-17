using Application.Features.Public.ProductsGroups.Dtos;
using Domain.ProductGroups.Products;

namespace Application.Features.Public.ProductsGroups.Queries.GetProductsGroupById;

internal sealed class GetProductsGroupByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetProductsGroupByIdQuery, Result<ProductsGroupDto>>
{
    public async Task<Result<ProductsGroupDto>> Handle(GetProductsGroupByIdQuery request, CancellationToken ct)
    {
        ProductGroupId productGroupId = request.ProductGroupId;
        var products = await context.Products
                    .AsNoTracking()
                    .Where(p => p.ProductGroupId == productGroupId && p.Status == ProductState.Published)
                    .Select(p => new ProductDto(
                        p.Id,
                        p.OriginalPrice,
                        p.HasActiveDiscount,
                        p.DiscountPercentage,
                        p.PriceAfterDiscount,
                        p.Images,
                        p.Slug,
                        p.Specifications,
                        p.StockPerWarehouse.Any(stock => stock.Quantity > 0)
                    ))
                    .ToListAsync(ct);

        var productGroupDto = await context.ProductGroups
            .AsNoTracking()
            .Where(x => x.Id == productGroupId && x.Status == ProductGroupState.Published)
            .Select(x => new ProductsGroupDto(
                x.Id,
                x.FeaturedProductId,
                x.Title,
                x.Description,
                x.Attributes,
                x.BrandName,
                x.CategoryName,
                x.AverageRating,
                products
            ))
            .FirstOrDefaultAsync(ct);


        if (productGroupDto is null)
        {
            return ApplicationErrors.NotFound.Product;
        }

        return productGroupDto;

    }
}
