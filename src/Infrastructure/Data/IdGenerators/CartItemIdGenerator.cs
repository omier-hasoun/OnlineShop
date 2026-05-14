using Domain.Carts.CartItems;
using Domain.Orders;
using App = Application.Common.Abstractions;
namespace Infrastructure.Data.IdGenerators;

internal sealed class CartItemIdGenerator([FromKeyedServices("Snowflake")] IPrimitiveTypeIdGenerator<long> Generator) : App.IIdGenerator<CartItemId>
{
    public CartItemId NewId()
    {
        return new CartItemId(Generator.Generate());
    }
}
