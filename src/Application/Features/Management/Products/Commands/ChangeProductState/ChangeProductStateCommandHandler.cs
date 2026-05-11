namespace Application.Features.Management.Products.Commands.ChangeProductState;

internal sealed class ChangeProductStateCommandHandler(IAppDbContext context) : IRequestHandler<ChangeProductStateCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(ChangeProductStateCommand request, CancellationToken ct)
    {
        var productId = new ProductId(request.ProductId);

        if (!Enum.TryParse<ProductStatus>(request.Status, ignoreCase: true, out var status))
        {
            return ApplicationErrors.Validation.ProductStatusInvalid;
        }

        var product = await context.Products.Include(x => x.Variants).FirstOrDefaultAsync(x => x.Id == productId, ct);

        if (product is null)
        {
            return ApplicationErrors.NotFound.Product;
        }

        var res = product.ChangeStatus(status);

        if (res.Failed)
            return res.Errors;

        await context.SaveAsync(ct);

        return Result.Updated;
    }

}
