using Domain.Common.ValueObjects;
using Domain.ProductGroups.Products;

namespace Application.Features.Management.ProductGroups.Commands.AddProduct;

public sealed class AddProductCommandHandler( IAppDbContext context, IIdGenerator<ProductId> idGen) : IRequestHandler<AddProductCommand, Result<long>>

{
    public async Task<Result<long>> Handle(AddProductCommand command, CancellationToken ct)
    {

        ProductGroupId productGroupId = new(command.ProductId);

        var price = Money.From((decimal)command.Price).Value;

        var product = await context.ProductGroups.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == productGroupId, ct);

        if (product is null)
        {
            return ApplicationErrors.NotFound.Product;
        }

        var productId = idGen.NewId();

        var createVariantResult = product.AddProduct(productId, price,
            command.Width, command.Height, command.Length, command.Weight,
            command.Sku, command.Slug, command.BarCode, command.Specifications);

        if (createVariantResult.Failed)
        {
            return createVariantResult.Errors;
        }

        await context.SaveAsync(ct);


        return productId.Value;
    }


}
