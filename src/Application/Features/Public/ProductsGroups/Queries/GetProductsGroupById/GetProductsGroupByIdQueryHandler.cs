using Application.Features.Public.ProductsGroups.Dtos;

namespace Application.Features.Public.ProductsGroups.Queries.GetProductsGroupById;

internal sealed class GetProductsGroupByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetProductsGroupByIdQuery, Result<ProductsGroupDto>>
{
    public async Task<Result<ProductsGroupDto>> Handle(GetProductsGroupByIdQuery request, CancellationToken ct)
    {
        ProductGroupId productGroupId = request.ProductGroupId;

        var query = context.ProductGroups.AsNoTracking()
                                    .Where(x => x.Id == productGroupId && x.Status == ProductGroupState.Published)
                                    .Select(
                                            p =>
                                               new ProductsGroupDto(
                                               p.Id,
                                               p.FeaturedProductId,
                                               p.Title,
                                               p.Description,
                                               p.Attributes.ToDictionary(),
                                               p.BrandName,
                                               p.CategoryName,
                                               p.AverageRating,
                                               p.Products.Select(x => new ProductDto(
                                                   id: x.Id,
                                                   price: x.Price,
                                                   hasActiveDiscount: x.HasActiveDiscount,
                                                   discountPercentage: x.DiscountPercentage,
                                                   priceAfterDiscount: x.PriceAfterDiscount,
                                                   images: x.Images,
                                                   slug: x.Slug,
                                                   specifications: x.Specifications.ToDictionary(),
                                                   isAvailable: x.StockPerWarehouse.Any(x => x.Quantity > 0)

                                               )).ToList()
                                            )

                                    );

        ProductsGroupDto? productGroupDto = await query.FirstOrDefaultAsync(ct);

        if (productGroupDto is null)
        {
            return ApplicationErrors.NotFound.Product;
        }

        return productGroupDto;

    }
}
