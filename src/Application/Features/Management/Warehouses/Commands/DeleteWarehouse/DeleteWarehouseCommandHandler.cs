
namespace Application.Features.Management.Warehouses.Commands.DeleteWarehouse;

internal class DeleteWarehouseCommandHandler(IAppDbContext context) : IRequestHandler<DeleteWarehouseCommand, Result<Deleted>>
{
    public async Task<Result<Deleted>> Handle(DeleteWarehouseCommand request, CancellationToken ct)
    {
        var warehouse = await context.Warehouses.FirstOrDefaultAsync(x => x.Id == request.ParsedWarehouseId);

        if (warehouse is null)
            return ApplicationErrors.NotFound.Warehouse;

        if (ct.IsCancellationRequested)
            return ApplicationErrors.OperationWasCanceled;

        return await context.Addresses.Where(x => x.Id == warehouse.AddressId).ExecuteDeleteAsync(ct) > 0 ?
        Result.Deleted : ApplicationErrors.DeleteOperationFailed;
    }
}
