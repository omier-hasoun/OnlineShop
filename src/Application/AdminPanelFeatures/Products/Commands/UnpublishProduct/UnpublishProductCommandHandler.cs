
using Domain.Products.ProductVariants;

namespace Application.AdminPanelFeatures.Products.Commands.PublishProduct;

internal sealed class UnpublishProductCommandHandler(IAppDbContext context) : IRequestHandler<UnpublishProductCommand, Result<Success>>
{

    public async Task<Result<Success>> Handle(UnpublishProductCommand request, CancellationToken ct)
    {
        ProductId productId = new(request.Product_Id);
        ProductVariantId? variantId = request.Variant_Id is null ? null : new((long)request.Variant_Id);

        var product = await context.Products.Include(x => x.Variants).FirstOrDefaultAsync(x => x.Id == productId, ct);

        if (product is null)
        {
            return ApplicationErrors.NotFound.Product;
        }
        
        if (variantId is null)
        {
            var result = product.Unpublish();

            if (result.Failed)
            {
                return result.Errors;
            }
        }
        else
        {
            var result = product.UnpublishVariant((ProductVariantId)variantId);

            if (result.Failed)
            {
                return result.Errors;
            }
        }



        await context.SaveAsync(ct);
        
        return Result.Success;
    }
}
