using Domain.Brands;
using Domain.Categories;

namespace Application.Features.Management.Products.Commands.UpdateProduct;

internal sealed class CreateProductCommandHandler(IAppDbContext context) : IRequestHandler<UpdateProductCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UpdateProductCommand request, CancellationToken ct)
    {

        ProductId productId = new(request.Product_Id);

        var product = await context.Products.FindAsync(productId);

        if (product is null)
        {
            return ApplicationErrors.NotFound.Product;
        }


        BrandId? brandId = request.New_Brand_Id  is null ? null : new (request.New_Brand_Id.Value);

        CategoryId? categoryId = request.New_Category_Id is null ? null : new(request.New_Category_Id.Value);

        var updateResult = product.Update(brandId, categoryId, request.New_Title, request.New_Description, request.New_Is_Serialized, request.New_Attributes);

        if (updateResult.Failed)
            return updateResult.Errors;

        await context.SaveAsync(ct);

        return Result.Updated;
    }

}
