
using System.ComponentModel;
using Domain.Common.ValueObjects;

namespace Domain.Orders.OrderItems;

public sealed class OrderItem : BaseEntity<OrderItemId>
{
    private OrderItem(OrderItemId id, OrderId orderId, ProductVariantId productVariantId, short quantity, Money unitPrice, OrderItemStatus status)
        : base(id)
    {
        OrderId = orderId;
        ProductVariantId = productVariantId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Status = status;
    }
    internal static Result<OrderItem> Create(OrderItemId id, OrderId orderId, ProductVariantId productVariantId, short quantity, Money unitPrice)
    {

        return new OrderItem(id, orderId, productVariantId, quantity, unitPrice, OrderItemStatus.Pending);
    }

    public OrderId OrderId { get; private init; }
    public ProductVariantId ProductVariantId { get; private init; }

    public short Quantity { get; private init; }

    public Money UnitPrice { get;  }
    public Money TotalPrice { get; }

    public OrderItemStatus Status
    {
        get; private set
        {
            if (!Enum.IsDefined(typeof(OrderItemStatus), value))
            {
                throw new InvalidEnumArgumentException(nameof(Status), (int)value, typeof(OrderItemStatus));
            }
            field = value;
        }
    }

    private List<string> _serialNumbers = [];
    public IReadOnlyList<string> SerialNumbers { get{ return _serialNumbers.AsReadOnly(); } private set{_serialNumbers = value is null ?[] : value.ToList();} }

    internal Result<Success> UpdateSerialNumbers(IReadOnlyCollection<string> serialNumbers)
    {
        ArgumentNullException.ThrowIfNull(serialNumbers, nameof(serialNumbers));

        if (serialNumbers.Count != Quantity)
        {
            return OrderItemErrors.SerialNumbersDoNotMatchQuantity;
        }
        _serialNumbers = serialNumbers.ToList();
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
            return OrderItemErrors.CannotConfirm;
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
            return OrderItemErrors.CannotCancel;
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
            return OrderItemErrors.CannotReturn;
        }

        return Result.Success;
    }

    internal Result<Success> MarkAsShipped()
    {

        if (Status is not OrderItemStatus.Shipped and not OrderItemStatus.Delivered)
        {
            return OrderItemErrors.CannotReturn;
        }

        return Result.Success;
    }
}
