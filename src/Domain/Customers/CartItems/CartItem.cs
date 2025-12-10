namespace Domain.Customers.CartItems;

public sealed class CartItem : BaseEntity
{

    private CartItem()
    {

    }

    public static CartItem Create(CustomerId customerId, ProductId productId, short units)
    {
        return new()
        {
            CustomerId = customerId,
            ProductId = productId,
            Units = units,
        };
    }
    public short Units { get; private set; }
    public CustomerId CustomerId { get; private init; }
    public ProductId ProductId { get; private init; }

    public Product ProductInfo {get; private set;}
}
