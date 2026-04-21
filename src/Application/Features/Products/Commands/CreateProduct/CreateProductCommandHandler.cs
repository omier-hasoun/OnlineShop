
namespace Application.Features.Products.Commands.CreateProduct;

public sealed class CreateProductCommandHandler(IAppDbContext context, IIdGenerator<ProductId> idGen) : IRequestHandler<CreateProductCommand, Result<ProductId>>
{
    public async Task<Result<ProductId>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (ProductNameAlreadyExists(request.Title))
        {
            return ProductApplicationErrors.ProductNameAlreadyExists;
        }

        var createProductResult = Product.Create(
            idGen.NewId(),
            request.BrandId,
            request.CategoryId,
            request.Title,
            request.Description,
            request.DefaultOriginalPrice,
            request.ContainsAlcohol,
            request.IsDangerousGood,
            request.IsBiological,
            request.IsSerialized,
            request.MaxQuantityPerCustomer,
            request.Attributes
        );

        if (createProductResult.Failed)
        {
            return createProductResult.Errors;
        }

        context.Products.Add(createProductResult.Value);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result > 0
            ? createProductResult.Value.Id
            : ProductApplicationErrors.ProductCreationFailed;
    }

    private bool ProductNameAlreadyExists(string name)
    {
        return context.Products.Any(p => p.Title == name);
    }
}
