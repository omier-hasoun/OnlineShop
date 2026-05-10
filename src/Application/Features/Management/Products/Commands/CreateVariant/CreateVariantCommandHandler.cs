using Domain.Common.ValueObjects;
using Domain.Products.ProductVariants;

namespace Application.Features.Management.Products.Commands.CreateVariant;

public sealed class CreateVariantCommandHandler( IAppDbContext context, IIdGenerator<ProductVariantId> idGen) : IRequestHandler<CreateVariantCommand, Result<long>>

{
    public async Task<Result<long>> Handle(CreateVariantCommand command, CancellationToken ct)
    {

        ProductId productId = new(command.Product_Id);

        Money price = Money.From(command.Price).Value;

        var product = await context.Products.Include(x => x.Variants).FirstOrDefaultAsync(x => x.Id == productId, ct);

        if (product is null)
        {
            return ApplicationErrors.NotFound.Product;
        }

        var variandId = idGen.NewId();

        var createVariantResult = product.AddVariant(variandId, price,
            command.Width, command.Height, command.Length, command.Weight,
            command.Sku, command.Slug, command.BarCode, command.Specifications);

        if (createVariantResult.Failed)
        {
            return createVariantResult.Errors;
        }

        await context.SaveAsync(ct);


        return variandId.Value;
    }


}
