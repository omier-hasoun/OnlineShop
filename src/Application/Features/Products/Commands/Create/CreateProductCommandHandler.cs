using Microsoft.Extensions.DependencyInjection;

namespace Application.Features.Products.Commands.Create;

public sealed class CreateProductCommandHandler(IAppDbContext context, [FromKeyedServices(IdProviderTypes.Snowflake)]IIdProvider<long> idGen) : IRequestHandler<CreateProductCommand, Result<ProductId>>
{
    public async Task<Result<ProductId>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (ProductNameAlreadyExists(request.Title))
        {
            return ProductApplicationErrors.ProductNameAlreadyExists;
        }

        var createProductResult = Product.Create(
            idGen.GetNewId(),
            request.Title,
            request.Description,
            request.Brand
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
