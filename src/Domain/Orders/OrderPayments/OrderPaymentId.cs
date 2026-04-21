
namespace Domain.Orders.OrderPayments;

public readonly record struct OrderPaymentId
{
    public long Value { get; init; }

    public static implicit operator long(OrderPaymentId orderPaymentId) => orderPaymentId.Value;
    public static implicit operator OrderPaymentId(long value) => new(value);
    public OrderPaymentId(long value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("OrderPaymentId is invalid.", nameof(value));
        }
        Value = value;
    }
}


