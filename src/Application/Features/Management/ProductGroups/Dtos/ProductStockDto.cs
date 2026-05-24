
using Domain.Warehouses;

namespace Application.Features.Management.ProductGroups.Dtos;

public sealed record ProductStockDto(long WarehouseId, int StockQuantity)
{
    internal WarehouseId ParsedWarehouseId => new(WarehouseId);
}
