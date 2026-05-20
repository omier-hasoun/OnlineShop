
using Application.Features.Management.Warehouses.Dtos;

namespace Application.Features.Management.Warehouses.Queries.GetWarehouseById;

internal sealed class GetWarehouseByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetWarehouseByIdQuery, Result<WarehouseDto>>
{
    public async Task<Result<WarehouseDto>> Handle(GetWarehouseByIdQuery request, CancellationToken ct)
    {
        var warehouse = await context.Warehouses.AsNoTracking()
                                                .Include(x => x.Address)
                                                .FirstOrDefaultAsync(x => x.Id == request.ParsedWarehouseId, ct);

        if (warehouse is null)
            return ApplicationErrors.NotFound.Warehouse;

        return new WarehouseDto(warehouse);
    }
}
