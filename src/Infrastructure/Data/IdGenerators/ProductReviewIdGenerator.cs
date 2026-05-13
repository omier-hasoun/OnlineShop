using Domain.ProductGroups;
using App = Application.Common.Abstractions;
namespace Infrastructure.Data.IdGenerators;

internal sealed class ProductReviewIdGenerator([FromKeyedServices("Snowflake")] IPrimitiveTypeIdGenerator<long> Generator) : App.IIdGenerator<ProductGroupId>
{
    public ProductGroupId NewId()
    {
        return new ProductGroupId(Generator.Generate());
    }
}
