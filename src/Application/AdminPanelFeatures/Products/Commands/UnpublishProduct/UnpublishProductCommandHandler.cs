
using Application.Common.Exceptions;

namespace Application.AdminPanelFeatures.Products.Commands.PublishProduct;

internal sealed class UnpublishProductCommandHandler(IAppDbContext context) : IRequestHandler<UnpublishProductCommand, Result<Success>>
{

    public async Task<Result<Success>> Handle(UnpublishProductCommand request, CancellationToken ct)
    {
        var product = await context.Products.Include(x => x.Variants).FirstOrDefaultAsync(x => x.Id == request.ProductId, ct);
        if (product is null)
        {
            return ApplicationErrors.NotFound.Product;
        }

        var result = product.Unpublish();

        if(result.Failed)
        {
            return result.Errors;
        }

        if (await context.SaveAsync(ct))
            return Result.Success;

        throw new DbSaveFailedException();
    }
}
