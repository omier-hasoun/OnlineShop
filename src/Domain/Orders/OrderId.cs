

namespace Domain.Orders;

public readonly record struct OrderId
{
    public long Value { get; }
    public static implicit operator long(OrderId orderId) => orderId.Value;
    public static implicit operator OrderId(long value) => new(value);
    public OrderId(long value)
    {
        if (Value <= 0)
            throw new ArgumentException("OrderId is invalid.", nameof(Value));
        Value = value;
    }
}
