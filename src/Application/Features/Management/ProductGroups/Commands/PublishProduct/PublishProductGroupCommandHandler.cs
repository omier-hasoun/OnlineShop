
namespace Application.Features.Management.ProductGroups.Commands.PublishProduct;

internal sealed class PublishProductGroupCommandHandler(IAppDbContext context) : IRequestHandler<PublishProductGroupCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(PublishProductGroupCommand command, CancellationToken ct)
    {
        var productsGroupId = command.ParsedProductsGroupId;

        var productGroup = await context.ProductGroups.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == productsGroupId, ct);

        if (productGroup is null)
        {
            return ApplicationErrors.NotFound.ProductGroup;
        }

        var res = productGroup.PublishGroup();

        if (res.Failed)
            return res.Errors;

        await context.SaveAsync(ct);

        return Result.Success;
    }
}
