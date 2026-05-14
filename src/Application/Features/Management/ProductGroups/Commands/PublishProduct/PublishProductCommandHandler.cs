

namespace Application.Features.Management.ProductGroups.Commands.PublishProduct;

internal sealed class PublishProductCommandHandler(IAppDbContext context) : IRequestHandler<PublishProductCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(PublishProductCommand command, CancellationToken ct)
    {
        var productGroupId = command.ParsedProductGroupId;

        var productGroup = await context.ProductGroups.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == productGroupId, ct);

        if (productGroup is null)
        {
            return ApplicationErrors.NotFound.ProductGroup;
        }

        var res = productGroup.PublishProduct(command.ParsedProductId);

        if (res.Failed)
            return res.Errors;

        await context.SaveAsync(ct);

        return Result.Success;
    }
}
