
using Domain.Products.ProductVariants;

namespace Application.Features.Management.Products.Commands.ChangeVariantState;

internal sealed class ChangeVariantStateCommandHandler(IAppDbContext context) : IRequestHandler<ChangeVariantStateCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(ChangeVariantStateCommand command, CancellationToken ct)
    {
        ProductId productId = new(command.ProductId);

        if (!Enum.TryParse<ProductStatus>(command.Status, ignoreCase: true, out var status))
        {
            return ApplicationErrors.Validation.ProductStatusInvalid;
        }

        var product = await context.Products.Include(x => x.Variants).FirstOrDefaultAsync(x => x.Id == productId, ct);

        if (product is null)
        {
            return ApplicationErrors.NotFound.Product;
        }

        ProductVariantId variantId = new(command.VariantId);

        var result = product.ChangeVariantStatus(variantId, status);

        if (result.Failed)
        {
            return result.Errors;
        }

        await context.SaveAsync(ct);

        return Result.Success;
    }
}
