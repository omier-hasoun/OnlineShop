
namespace Application.Features.Management.Warehouses.Commands.DeleteWarehouse;

internal class DeleteWarehouseCommandHandler(IAppDbContext context) : IRequestHandler<DeleteWarehouseCommand, Result<Deleted>>
{
    public async Task<Result<Deleted>> Handle(DeleteWarehouseCommand request, CancellationToken ct)
    {
        var warehouse = await context.Warehouses.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == request.ParsedWarehouseId, ct);

        if (warehouse is null)
            return ApplicationErrors.NotFound.Warehouse;

        context.Addresses.Remove(warehouse.Address); // this will delete warehouse too (cascade delete)

        await context.SaveAsync(ct);

        return Result.Deleted;
    }
}
