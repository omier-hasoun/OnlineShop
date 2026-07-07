using Domain.Orders.OrderLines;
using App = Application.Common.Abstractions;
namespace Infrastructure.Data.IdGenerators;

internal sealed class OrderLineIdGenerator([FromKeyedServices("Snowflake")] IPrimitiveTypeIdGenerator<long> Generator) : App.IIdGenerator<OrderLineId>
{
    public OrderLineId NewId()
    {
        return new OrderLineId(Generator.Generate());
    }
}
