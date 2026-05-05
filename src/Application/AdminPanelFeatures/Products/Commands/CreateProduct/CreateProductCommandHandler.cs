
using Domain.Brands;
using Domain.Categories;

namespace Application.AdminPanelFeatures.Products.Commands.CreateProduct;

internal sealed class CreateProductCommandHandler(IAppDbContext context, IIdGenerator<ProductId> idGen) : IRequestHandler<CreateProductCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateProductCommand request, CancellationToken ct)
    {

        ProductId productId = idGen.NewId();

        BrandId brandId = new(request.Brand_Id);

        CategoryId categoryId = new(request.Category_Id);

        var createProductResult = Product.Create(
            productId,
            brandId,
            categoryId,
            request.Title,
            request.Description,
            request.Is_Serialized,
            request.Attributes
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
