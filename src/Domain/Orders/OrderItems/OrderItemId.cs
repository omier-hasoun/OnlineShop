
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

    public static OrderItemId Parse(string value)
    {
        if (TryParse(value, out var id))
            return id;
        throw new ArgumentException("ProductReviewId is invalid.", nameof(value));
    }
    public static bool TryParse(string value, out OrderItemId id)
    {
        if (long.TryParse(value, out var brandId))
        {
            id = new OrderItemId(brandId);
            return true;
        }
        id = new();
        return false;
    }
}
