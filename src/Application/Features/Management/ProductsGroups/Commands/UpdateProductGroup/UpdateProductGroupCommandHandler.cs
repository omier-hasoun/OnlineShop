using Domain.Brands;
using Domain.Categories;

namespace Application.Features.Management.ProductsGroups.Commands.UpdateProductGroup;

internal sealed class UpdateProductGroupCommandHandler(IAppDbContext context) : IRequestHandler<UpdateProductGroupCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UpdateProductGroupCommand command, CancellationToken ct)
    {

        ProductsGroupId productId = new(command.ProductId);

        if (!command.HasChanges())
        {
            return Result.Updated;
        }

        var product = await context.ProductGroups.FindAsync(productId);

        if (product is null)
        {
            return ApplicationErrors.NotFound.Product;
        }


        BrandId? brandId = command.BrandId  is null ? null : new (command.BrandId.Value);

        CategoryId? categoryId = command.CategoryId is null ? null : new(command.CategoryId.Value);

        var updateResult = product.Update(brandId, categoryId, command.Title, command.Description, command.IsSerialized, command.Attributes);

        if (updateResult.Failed)
            return updateResult.Errors;

        await context.SaveAsync(ct);

        return Result.Updated;
    }

}
