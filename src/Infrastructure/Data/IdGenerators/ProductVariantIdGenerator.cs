using Domain.Products;
using Domain.Products.ProductVariants;
using App = Application.Common.Abstractions;
namespace Infrastructure.Data.IdGenerators;

internal sealed class ProductVariantIdGenerator([FromKeyedServices("Snowflake")] IPrimitiveTypeIdGenerator<long> Generator) : App.IIdGenerator<ProductVariantId>
{
    public ProductVariantId NewId()
    {
        return new ProductVariantId(Generator.Generate());
    }
}
