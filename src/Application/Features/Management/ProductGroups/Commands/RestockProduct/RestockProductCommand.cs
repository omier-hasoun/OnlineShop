
using Domain.ProductGroups.Products;
using Domain.Warehouses;

namespace Application.Features.Management.ProductGroups.Commands.RestockProduct;

public sealed record RestockProductCommand(long warehouseId, long productId, int StockQuantity) : IRequest<Result<Updated>>
{
    internal WarehouseId WarehouseId => new(warehouseId);
    internal ProductId ProductId => new(productId);

}
