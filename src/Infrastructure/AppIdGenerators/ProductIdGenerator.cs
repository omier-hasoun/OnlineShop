using Domain.Products;
using App = Application.Common.Abstractions;
namespace Infrastructure.AppIdGenerators;

internal sealed class ProductIdGenerator([FromKeyedServices("Snowflake")] IPrimitiveTypeIdGenerator<long> Generator) : App.IIdGenerator<ProductId>
{
    public ProductId NewId()
    {
        return Generator.Generate();
    }
}
