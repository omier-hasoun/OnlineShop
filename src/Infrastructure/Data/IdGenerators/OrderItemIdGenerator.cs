using Domain.ProductsGroups;
using App = Application.Common.Abstractions;
namespace Infrastructure.Data.IdGenerators;

internal sealed class OrderItemIdGenerator([FromKeyedServices("Snowflake")] IPrimitiveTypeIdGenerator<long> Generator) : App.IIdGenerator<ProductsGroupId>
{
    public ProductsGroupId NewId()
    {
        return new ProductsGroupId(Generator.Generate());
    }
}
