
using Application.Common.Dtos;
using Domain.Warehouses;

namespace Application.Features.Management.Warehouses.Commands.CreateWarehouse;

public sealed record CreateWarehouseCommand(string WarehouseName, AddressRequest Address) : IRequest<Result<long>>
{
}
