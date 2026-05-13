
namespace Application.Features.Management.ProductGroups.Commands.ArchiveProductGroup;

internal sealed class ArchiveProductCommandHandler(IAppDbContext context) : IRequestHandler<ArchiveProductCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(ArchiveProductCommand command, CancellationToken ct)
    {
        throw new NotImplementedException();
        var productGroupId = command.ParsedProductGroupId;
        var productId = command.ParsedProductId;

        var productGroup = await context.ProductGroups.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == productGroupId, ct);

        if (productGroup is null)
        {
            return ApplicationErrors.NotFound.ProductGroup;
        }
        //ProductGroup
        //var res = productGroup.;

        //if (res.Failed)
        //    return res.Errors;

        //await context.SaveAsync(ct);

        return Result.Success;
    }
}
