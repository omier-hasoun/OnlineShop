using Domain.Brands;
using Domain.Categories;

namespace Application.Features.Management.ProductGroups.Commands.CreateProductGroup;

internal sealed class CreateProductGroupCommandHandler(IAppDbContext context, IIdGenerator<ProductGroupId> idGen) : IRequestHandler<CreateProductGroupCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateProductGroupCommand command, CancellationToken ct)
    {


        BrandId brandId = new(command.BrandId);

        CategoryId categoryId = new(command.CategoryId);

        ProductGroupId productId = idGen.NewId();

        var createProductResult = ProductGroup.Create(
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

        context.ProductGroups.Add(createProductResult.Value);

        await context.SaveAsync(ct);
            
        return productId.Value;
    }
    
}
