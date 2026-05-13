using Domain.ProductsGroups;
using Domain.ProductsGroups.Products;
using App = Application.Common.Abstractions;
namespace Infrastructure.Data.IdGenerators;

internal sealed class ProductIdGenerator([FromKeyedServices("Snowflake")] IPrimitiveTypeIdGenerator<long> Generator) : App.IIdGenerator<ProductId>
{
    public ProductId NewId()
    {
        return new ProductId(Generator.Generate());
    }
}
