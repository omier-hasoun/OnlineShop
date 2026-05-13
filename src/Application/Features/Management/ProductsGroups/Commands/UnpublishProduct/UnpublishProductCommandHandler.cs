
namespace Application.Features.Management.ProductsGroups.Commands.UnpublishProduct;

internal sealed class UnpublishProductCommandHandler(IAppDbContext context) : IRequestHandler<UnpublishProductCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(UnpublishProductCommand command, CancellationToken ct)
    {
        var productGroupId = command.ParsedProductGroupId;

        var productGroup = await context.ProductGroups.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == productGroupId, ct);

        if (productGroup is null)
        {
            return ApplicationErrors.NotFound.ProductGroup;
        }

        var res = productGroup.UnpublishProduct(command.ParsedProductId);

        if (res.Failed)
            return res.Errors;

        await context.SaveAsync(ct);

        return Result.Success;
    }
}
