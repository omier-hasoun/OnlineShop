
namespace Domain.Orders.OrderItems;

public readonly record struct OrderItemId
{
    public long Value { get; init; }
    
    public OrderItemId(long value)
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
