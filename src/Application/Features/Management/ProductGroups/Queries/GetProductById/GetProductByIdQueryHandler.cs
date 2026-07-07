
using Application.Features.Management.ProductGroups.Dtos;

namespace Application.Features.Management.ProductGroups.Queries.GetProductById;

internal sealed class GetProductByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken ct)
    {
        var product = await context.Products.AsNoTracking()
                                            .Include(x => x.Inventories)
                                            .ThenInclude(x => x.Warehouse)
                                            .FirstOrDefaultAsync(x => x.Id == request.ParsedProductId, ct);

        if (product is null)
            return ApplicationErrors.NotFound.Product;

        return new ProductDto(
                product.Id,
                product.ProductGroupId,
                product.PriceAfterDiscount,
                product.OriginalPrice,
                product.DiscountPercentage,
                product.DiscountExpiresOn,
                product.Status,
                product.Width,
                product.Height,
                product.Length,
                product.Weight,
                product.Sku,
                product.Slug,
                product.BarCode,
                product.HasActiveDiscount,
                product.Specifications.ToDictionary(),
                [.. product.Images],
                product.Inventories.Select(x => new ProductInventoryDto(
                                                    x.WarehouseId, x.Warehouse.Name, x.StockQuantity)).ToList()
            );
    }
}
