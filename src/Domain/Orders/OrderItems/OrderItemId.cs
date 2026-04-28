
namespace Domain.Orders.OrderItems;

public readonly record struct OrderItemId
{
    public long Value { get; init; }

    public static implicit operator long(OrderItemId orderItemId) => orderItemId.Value;
    public static implicit operator OrderItemId(long value) => new(value);
    public OrderItemId(long value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("OrderItemId is invalid.", nameof(value));
        }
        Value = value;
    }

    public static Result<CategoryId> From(long value)
    {
        if (value <= 0)
        {
            return new CategoryId(value);
        }

        return DomainErrors.Categories.CategoryIdInvalid;
    }
}
