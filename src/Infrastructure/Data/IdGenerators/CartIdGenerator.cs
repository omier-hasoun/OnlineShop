using Domain.Carts;
using App = Application.Common.Abstractions;
namespace Infrastructure.Data.IdGenerators;

internal sealed class CartIdGenerator([FromKeyedServices("Snowflake")] IPrimitiveTypeIdGenerator<long> Generator) : App.IIdGenerator<CartId>
{
    public CartId NewId()
    {
        return new CartId(Generator.Generate());
    }
}
