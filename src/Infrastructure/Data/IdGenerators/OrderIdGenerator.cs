using Domain.Orders;
using App = Application.Common.Abstractions;
namespace Infrastructure.Data.IdGenerators;

internal sealed class OrderIdGenerator([FromKeyedServices("Snowflake")] IPrimitiveTypeIdGenerator<long> Generator) : App.IIdGenerator<OrderId>
{
    public OrderId NewId()
    {
        return new OrderId(Generator.Generate());
    }
}
