
namespace Application.Features.Management.ProductGroups.Commands.ArchiveProductGroup;

internal sealed class ArchiveProductGroupCommandHandler(IAppDbContext context) : IRequestHandler<ArchiveProductGroupCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(ArchiveProductGroupCommand command, CancellationToken ct)
    {
        var productId = command.ParsedProductId;

        var productGroup = await context.ProductGroups.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == productId, ct);

        if (productGroup is null)
        {
            return ApplicationErrors.NotFound.ProductGroup;
        }

        var res = productGroup.ArchiveGroup();

        if (res.Failed)
            return res.Errors;

        await context.SaveAsync(ct);

        return Result.Success;
    }

}
