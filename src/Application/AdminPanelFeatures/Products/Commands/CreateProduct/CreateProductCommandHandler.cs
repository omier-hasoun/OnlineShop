
using Application.Common.Exceptions;

namespace Application.AdminPanelFeatures.Products.Commands.CreateProduct;

internal sealed class CreateProductCommandHandler(IAppDbContext context, IIdGenerator<ProductId> idGen) : IRequestHandler<CreateProductCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateProductCommand request, CancellationToken ct)
    {

        ProductId productId = idGen.NewId();

        var createProductResult = Product.Create(
            productId,
            request.BrandId,
            request.CategoryId,
            request.Title,
            request.Description,
            request.IsSerialized,
            request.Attributes
        );

        if (createProductResult.Failed)
        {
            return createProductResult.Errors;
        }

        context.Products.Add(createProductResult.Value);

        var succeeded = await context.SaveAsync(ct);

        if(succeeded)
        {
            return productId.Value;
        }

        throw new DbSaveFailedException();
    }

    //private bool IsUniqueProductTitle(string name)
    //{
    //    return context.Products.Any(p => p.Title == name);
    //}
}
