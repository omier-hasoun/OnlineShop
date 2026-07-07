
namespace Domain.Orders.OrderLines;

public readonly record struct OrderLineId
{
    public long Value { get; init; }
    
    public OrderLineId(long value)
    {
        Value = value;
    }

    public Result<Success> IsValid()
    {
        if (Value <= 0)
        {
            return DomainErrors.Orders.OrderItemIdInvalid;
        }

        return Result.Success;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
