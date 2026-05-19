
using Domain.Warehouses;

namespace Application.Features.Management.Warehouses.Commands.CreateWarehouse;

public sealed record CreateWarehouseCommand(string WarehouseName) : IRequest<Result<WarehouseId>>
{
}
