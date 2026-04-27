
using Domain.Brands;
using Domain.Categories;
using Domain.Common.ValueObjects;

namespace Application.Features.Products.Commands.CreateProduct;

internal sealed class CreateProductCommandHandler(IAppDbContext context, IIdGenerator<ProductId> idGen) : IRequestHandler<CreateProductCommand, Result<ProductId>>
{
    public async Task<Result<ProductId>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {


        var createProductResult = Product.Create(
            idGen.NewId(),
            BrandId.Parse(request.BrandId),
            CategoryId.Parse(request.CategoryId),
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

        var result = await context.SaveChangesAsync(cancellationToken);

        return result > 0 ?
            createProductResult.Value.Id :
            ProductApplicationErrors.ProductCreationFailed;
    }

    //private bool IsUniqueProductTitle(string name)
    //{
    //    return context.Products.Any(p => p.Title == name);
    //}
}
