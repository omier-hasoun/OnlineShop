using Application.Features.Management.Warehouses.Dtos;
using Domain.Warehouses;

namespace Application.Features.Management.Warehouses.Commands.CreateWarehouse;

public sealed record CreateWarehouseCommand(string WarehouseName, WarehouseAddressRequest Address) : IRequest<Result<long>>
{
}
