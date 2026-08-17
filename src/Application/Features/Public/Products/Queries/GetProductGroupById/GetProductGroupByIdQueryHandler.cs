using Application.Features.Public.Products.Dtos;
using Domain.ProductGroups.Products;

namespace Application.Features.Public.Products.Queries.GetProductGroupById;

internal sealed class GetProductGroupByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetProductGroupByIdQuery, Result<ProductGroupDto>>
{
    public async Task<Result<ProductGroupDto>> Handle(GetProductGroupByIdQuery request, CancellationToken ct)
    {
        ProductGroupId productGroupId = request.ProductGroupId;

        var products = await context.Products
                    .AsNoTracking()
                    .Where(p => p.ProductGroupId == productGroupId && p.Status == ProductState.Published)
                    .Select(p => new ProductDto(
                        p.Id,
                        p.OriginalPrice,
                        p.HasDiscount,
                        p.DiscountPercentage,
                        p.DiscountPrice,
                        p.Images,
                        p.Slug,
                        p.Specifications,
                        inStock: p.Inventory.StockQuantity > 0
                    ))
                    .ToListAsync(ct);

        var productGroupDto = await context.ProductGroups
            .AsNoTracking()
            .Where(x => x.Id == productGroupId && x.Status == ProductGroupState.Published)
            .Select(x => new ProductGroupDto(
                x.Id,
                x.MainProductId,
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
