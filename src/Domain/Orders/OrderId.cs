
namespace Domain.Orders;

public readonly record struct OrderId
{
    public Guid Value { get; }
    public OrderId(Guid value)
    {
        if (Value == default)
            throw new ArgumentException("OrderId cannot be the default Guid.", nameof(Value));
        Value = value;
    }
}
