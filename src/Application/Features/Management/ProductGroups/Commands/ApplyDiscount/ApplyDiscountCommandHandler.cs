
using Domain.ProductsGroups.Products;

namespace Application.Features.Management.ProductGroups.Commands.ApplyDiscount;

internal sealed class ApplyDiscountCommandHandler(IAppDbContext context) : IRequestHandler<ApplyDiscountCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(ApplyDiscountCommand request, CancellationToken ct)
    {
        ProductGroupId productGroupId = request.ParsedProductGroupId;
        ProductId productId = request.ParsedProductId;

        var productGroup = await context.ProductGroups.Include(x => x.Products).FirstOrDefaultAsync(pg => pg.Id == productGroupId, ct);

        if (productGroup is null)
            return ApplicationErrors.NotFound.Product;

        var result = productGroup.ApplyDiscount(productId, request.DiscountExpiresOn, request.DiscountPercentage);

        if (result.Failed)
            return result;


        await context.SaveAsync(ct);
        return Result.Success;
    }
}
