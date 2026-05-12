

namespace Domain.Orders;

public readonly record struct OrderId
{
    public long Value { get; }
    public OrderId(long value)
    {
        Value = value;
    }

    public Result<Success> IsValid()
    {
        if (Value <= 0)
        {
            return DomainErrors.Orders.OrderIdInvalid;
        }

        return Result.Success;
    }
    public override string ToString()
    {
        return Value.ToString();
    }
}
