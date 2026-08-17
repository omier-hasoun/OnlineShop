
namespace Application.Features.Management.Warehouses.Commands.DeleteWarehouse;

internal class DeleteWarehouseCommandHandler(IAppDbContext context) : IRequestHandler<DeleteWarehouseCommand, Result<Deleted>>
{
    public async Task<Result<Deleted>> Handle(DeleteWarehouseCommand request, CancellationToken ct)
    {
        var warehouse = await context.Warehouses.AsNoTracking()
                                                .FirstOrDefaultAsync(x => x.Id == request.ParsedWarehouseId, ct);

        if (warehouse is null)
            return ApplicationErrors.NotFound.Warehouse;

        // deleting the address will also delete the warehouse because the warehouse depends on an address
        return await 
        context.Addresses.Where(x => x.Id == warehouse.AddressId)
                         .ExecuteDeleteAsync(ct) > 0 ? Result.Deleted : ApplicationErrors.DeleteOperationFailed;
    }
}
