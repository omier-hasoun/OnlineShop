
using Domain.Common.ValueObjects;
using Domain.Orders.ValueObjects;

namespace Domain.Orders.OrderItems;

public sealed class OrderItem : BaseEntity<OrderItemId>
{
    private OrderItem(OrderItemId id, OrderId orderId, ProductId productId, short quantity, Money unitPrice, OrderItemStatus status)
        : base(id)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Status = status;
    }
    internal static Result<OrderItem> Create(OrderItemId id, OrderId orderId, ProductId productId, short quantity, Money unitPrice)
    {

        return new OrderItem(id, orderId, productId, quantity, unitPrice, OrderItemStatus.Pending);
    }

    public OrderId OrderId { get; private init; }
    public ProductId ProductId { get; private init; }

    public short Quantity { get; private init; }

    public Money UnitPrice { get; } = null!;
    public Money TotalPrice { get; } = null!;

    public OrderItemStatus Status {get; private set;}

    public ProductInfoSnapShotAtPurchase ProductInfo { get; private set; } = null!;

    private List<string> _serialNumbers = [];
    public IReadOnlyList<string> SerialNumbers { get{ return _serialNumbers.AsReadOnly(); } private set{_serialNumbers = value is null ?[] : value.ToList();} }

    internal Result<Success> UpdateSerialNumbers(List<string> serialNumbers)
    {
        ArgumentNullException.ThrowIfNull(serialNumbers, nameof(serialNumbers));

        if (serialNumbers.Count != Quantity)
        {
            return DomainErrors.Orders.SerialNumbersOutOfRange;
        }

        _serialNumbers = serialNumbers;

        return Result.Success;
    }


    internal Result<Success> MarkAsConfirmed()
    {
        if(Status is OrderItemStatus.Confirmed)
        {
            return Result.Success;
        }

        if(Status != OrderItemStatus.Pending)
        {
            //return DomainErrors.OrderItems.CannotConfirm;
        }

        Status = OrderItemStatus.Confirmed;
        return Result.Success;
    }
    internal Result<Success> MarkAsCancelled()
    {
        if (Status is OrderItemStatus.Cancelled)
        {
            return Result.Success;
        }

        if (Status is not OrderItemStatus.Pending and not OrderItemStatus.Confirmed)
        {
            //return DomainErrors.OrderItems.CannotCancel;
        }

        Status = OrderItemStatus.Cancelled;
        return Result.Success;
    }

    internal Result<Success> MarkAsReturned()
    {
        if (Status is OrderItemStatus.Returned)
        {
            return Result.Success;
        }

        bool isValidStatusTransition = Status is OrderItemStatus.Delivered or OrderItemStatus.PartiallyReturned;
        if (isValidStatusTransition)
        {
            //return OrderItemErrors.CannotReturn;
        }

        return Result.Success;
    }

    internal Result<Success> MarkAsShipped()
    {

        if (Status is not OrderItemStatus.Shipped and not OrderItemStatus.Delivered)
        {
            //return OrderItemErrors.CannotReturn;
        }

        return Result.Success;
    }
}
