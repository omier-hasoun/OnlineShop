using Domain.Products;
using App = Application.Common.Abstractions;
namespace Infrastructure.Data.IdGenerators;

internal sealed class ProductReviewIdGenerator([FromKeyedServices("Snowflake")] IPrimitiveTypeIdGenerator<long> Generator) : App.IIdGenerator<ProductId>
{
    public ProductId NewId()
    {
        return new ProductId(Generator.Generate());
    }
}
