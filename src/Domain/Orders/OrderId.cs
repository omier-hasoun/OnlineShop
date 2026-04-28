

namespace Domain.Orders;

public readonly record struct OrderId
{
    public long Value { get; }
    public static implicit operator long(OrderId orderId) => orderId.Value;
    public static implicit operator OrderId(long value) => new(value);
    internal OrderId(long value)
    {
        Value = value;
    }

    public static Result<OrderId> From(long value)
    {
        if (value <= 0)
        {
            return new OrderId(value);
        }

        return DomainErrors.Orders.OrderIdInvalid;
    }
}
