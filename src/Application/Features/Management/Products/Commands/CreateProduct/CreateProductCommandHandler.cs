using Domain.Brands;
using Domain.Categories;

namespace Application.Features.Management.Products.Commands.CreateProduct;

internal sealed class CreateProductCommandHandler(IAppDbContext context, IIdGenerator<ProductId> idGen) : IRequestHandler<CreateProductCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateProductCommand command, CancellationToken ct)
    {


        BrandId brandId = new(command.BrandId);

        CategoryId categoryId = new(command.CategoryId);

        ProductId productId = idGen.NewId();

        var createProductResult = Product.Create(
            productId,
            brandId,
            categoryId,
            command.Title,
            command.Description,
            command.IsSerialized,
            command.Attributes
        );

        if (createProductResult.Failed)
        {
            return createProductResult.Errors;
        }

        context.Products.Add(createProductResult.Value);

        await context.SaveAsync(ct);
            
        return productId.Value;
    }
    
}
