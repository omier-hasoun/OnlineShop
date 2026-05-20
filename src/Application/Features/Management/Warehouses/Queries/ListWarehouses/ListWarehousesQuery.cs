

using Application.Features.Management.Warehouses.Dtos;

namespace Application.Features.Management.Warehouses.Queries.ListWarehouses;

public sealed record ListWarehousesQuery : IRequest<Result<List<WarehouseListItemDto>>>
{
}
