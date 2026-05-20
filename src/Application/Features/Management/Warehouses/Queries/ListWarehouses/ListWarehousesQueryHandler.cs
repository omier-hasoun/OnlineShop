
using Application.Features.Management.Warehouses.Dtos;

namespace Application.Features.Management.Warehouses.Queries.ListWarehouses;

internal sealed class ListWarehousesQueryHandler(IAppDbContext context) : IRequestHandler<ListWarehousesQuery, Result<List<WarehouseListItemDto>>>
{
    public async Task<Result<List<WarehouseListItemDto>>> Handle(ListWarehousesQuery request, CancellationToken ct)
    {
        var warehouses = await context.Warehouses.AsNoTracking()
                                      .Select(x => new WarehouseListItemDto(x.Id, x.Name))
                                      .ToListAsync(ct);
        return warehouses;
    }
}
