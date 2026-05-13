
namespace Application.Features.Management.ProductsGroups.Commands.UnpublishProduct;

internal sealed class UnpublishProductGroupCommandHandler(IAppDbContext context) : IRequestHandler<UnpublishProductGroupCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(UnpublishProductGroupCommand command, CancellationToken ct)
    {
        var productGroupId = command.ParsedProductGroupId;

        var productGroup = await context.ProductGroups.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == productGroupId, ct);

        if (productGroup is null)
        {
            return ApplicationErrors.NotFound.ProductGroup;
        }

        var res = productGroup.UnpublishGroup();

        if (res.Failed)
            return res.Errors;

        await context.SaveAsync(ct);

        return Result.Success;
    }
}
