
namespace Application.Features.Management.ProductGroups.Commands.ArchiveProductGroup;

internal sealed class ArchiveProductGroupCommandHandler(IAppDbContext context) : IRequestHandler<ArchiveProductGroupCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(ArchiveProductGroupCommand command, CancellationToken ct)
    {
        var productId = command.ParsedProductId;

        var productGroup = await context.ProductGroups.Include(x => x.Products)
                                                .ThenInclude(x => x.StockPerWarehouse)
                                                .FirstOrDefaultAsync(x => x.Id == productId, ct);

        if (productGroup is null)
        {
            return ApplicationErrors.NotFound.ProductGroup;
        }

        var res = productGroup.ArchiveGroup();

        if (res.Failed)
            return res.Errors;

        foreach ( var product in productGroup.Products)
        {
            foreach (var stock in product.StockPerWarehouse)
            {
                stock.ResetStock();
            }
        }

        await context.SaveAsync(ct);

        return Result.Success;
    }

}
