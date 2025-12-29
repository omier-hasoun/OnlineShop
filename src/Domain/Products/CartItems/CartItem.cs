namespace Domain.Products.CartItems;

public sealed class CartItem : BaseEntity
{

    private CartItem()
    {

    }

    internal static CartItem Create(CustomerId customerId, ProductId productId, ushort units = 1)
    {
        return new()
        {
            CustomerId = customerId,
            ProductId = productId,
            Units = (short)units,
        };
    }
    public short Units { get; private set; }
    public CustomerId CustomerId { get; private init; }
    public ProductId ProductId { get; private init; }

}
