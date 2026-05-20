

using Application.Features.Management.Warehouses.Dtos;
using Domain.Warehouses;

namespace Application.Features.Management.Warehouses.Queries.GetWarehouseById;

public sealed record GetWarehouseByIdQuery(long WarehouseId) : IRequest<Result<WarehouseDto>>
{
    public WarehouseId ParsedWarehouseId => new (WarehouseId);
}
