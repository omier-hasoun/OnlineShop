
namespace Application.AdminPanelFeatures.Products.Commands.PublishProduct;

internal sealed class PublishProductCommandHandler(IAppDbContext context) : IRequestHandler<PublishProductCommand, Result<Success>>
{

    public async Task<Result<Success>> Handle(PublishProductCommand request, CancellationToken ct)
    {
        var product = await context.Products.Include(x => x.Variants).FirstOrDefaultAsync(x => x.Id == request.ProductId, ct);

        if (product is null)
        {
            return ApplicationErrors.NotFound.Product;
        }

        var result = product.Publish();

        if (result.Failed)
        {
            return result.Errors;
        }

        await context.SaveAsync(ct);
        
        return Result.Success;
    }
}
