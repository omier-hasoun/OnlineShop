
using Application.Features.Management.ProductGroups.Dtos;

namespace Application.Features.Management.ProductGroups.Queries.GetProductById;

internal sealed class GetProductByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken ct)
    {
        var product = await context.Products.AsNoTracking()
                                            .Include(x => x.StockPerWarehouse)
                                            .ThenInclude(x => x.Warehouse)
                                            .FirstOrDefaultAsync(x => x.Id == request.ParsedProductId, ct);

        if (product is null)
            return ApplicationErrors.NotFound.Product;

        return new ProductDto(
                product.Id,
                product.ProductsGroupId,
                product.PriceAfterDiscount,
                product.Price,
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
                product.StockPerWarehouse.Select(x => new ProductInventoryDto(
                                                    x.WarehouseId, x.Warehouse.Name, x.Quantity)).ToList()
            );
    }
}
