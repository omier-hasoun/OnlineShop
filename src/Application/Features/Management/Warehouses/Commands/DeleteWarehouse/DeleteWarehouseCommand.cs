
using Domain.Warehouses;

namespace Application.Features.Management.Warehouses.Commands.DeleteWarehouse;

public sealed record class DeleteWarehouseCommand(long WarehouseId) : IRequest<Result<Deleted>>
{
    internal WarehouseId ParsedWarehouseId =>
        new(WarehouseId);
}
