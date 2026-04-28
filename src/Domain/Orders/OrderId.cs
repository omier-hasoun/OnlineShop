

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

    public static OrderId Parse(string value)
    {
        if (TryParse(value, out var id))
            return id;
        throw new ArgumentException("OrderId is invalid.", nameof(value));
    }
    public static bool TryParse(string value, out OrderId id)
    {
        if (long.TryParse(value, out var brandId))
        {
            id = new OrderId(brandId);
            return true;
        }
        id = new();
        return false;
    }
}
