
using System.ComponentModel;

namespace Domain.Orders.OrderItems;

public sealed class OrderItem : BaseEntity<OrderItemId>
{
    private OrderItem(OrderItemId id, OrderId orderId, ProductVariantId productVariantId, short quantity, decimal unitPrice, OrderItemStatus status)
        : base(id)
    {
        OrderId = orderId;
        ProductVariantId = productVariantId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Status = status;
    }
    internal static Result<OrderItem> Create(OrderItemId id, OrderId orderId, ProductVariantId productVariantId, short quantity, decimal unitPrice)
    {

        return new OrderItem(id, orderId, productVariantId, quantity, unitPrice, OrderItemStatus.Pending);
    }

    public OrderId OrderId { get; private init; }
    public ProductVariantId ProductVariantId { get; private init; }

    public short Quantity { get; private init; }
    public short ReturnedQuantity { get; private set; }

    public decimal UnitPrice { get; private init; }
    public decimal TotalPrice { get { return decimal.Multiply(Quantity, UnitPrice); } }

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

    public List<string> _serialNumbers = [];
    public IReadOnlyCollection<string> SerialNumbers { get{ return _serialNumbers.AsReadOnly(); } private set{_serialNumbers = value is null ?[] : value.ToList();} }

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

    internal Result<Success> MarkAsReturned(short quantityToReturn)
    {
        if (Status is OrderItemStatus.Returned)
        {
            return Result.Success;
        }

        bool isValidReturnQuantity = ValidationHelper.IsOutOfRange(quantityToReturn + ReturnedQuantity, 1, Quantity);
        if (isValidReturnQuantity)
        {
            return OrderItemErrors.InvalidReturnQuantity;
        }

        bool isValidStatusTransition = Status is OrderItemStatus.Delivered or OrderItemStatus.PartiallyReturned;
        if (isValidStatusTransition)
        {
            return OrderItemErrors.CannotReturn;
        }

        ReturnedQuantity += quantityToReturn;
        Status = ReturnedQuantity == Quantity ? OrderItemStatus.Returned : OrderItemStatus.PartiallyReturned;
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
