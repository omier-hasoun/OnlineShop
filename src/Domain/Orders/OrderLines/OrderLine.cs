
namespace Domain.Orders.OrderLines;

public sealed class OrderLine : BaseEntity<OrderLineId>
{
    private OrderLine(OrderLineId id, OrderId orderId, ProductId productId, short quantity, string productTitleSnapshot,
        Money unitPrice, Money total, OrderLineState status)
        : base(id)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        ProductTitleSnapshot = productTitleSnapshot;
        UnitPrice = unitPrice;
        Total = total;
        Status = status;
    }

    private static decimal CalculateTotalPrice(decimal unitPrice, short quantity)
        => unitPrice * quantity;
    
    internal static Result<OrderLine> Create(OrderLineId id, OrderId orderId, ProductId productId, string productTitleSnapshot,
        short quantity, Money unitPrice)
    {

        var validationResult = Result.ValidateAll(
                            () => id.IsValid(),
                            () => orderId.IsValid(),
                            () => productId.IsValid(),
                            () => ValidateQuantity(quantity),
                            () => ValidateProductTitle(productTitleSnapshot)
                        );

        if (validationResult.Failed)
            return validationResult.Errors;

        var total = Money.Create(CalculateTotalPrice(unitPrice.Value, quantity));

        return new OrderLine(id, orderId, productId, quantity, productTitleSnapshot, unitPrice, total, OrderLineState.Pending);
    }



    public OrderId OrderId { get; private init; }
    public ProductId ProductId { get; private init; }

    public string ProductTitleSnapshot { get; private init; }
    public short Quantity { get; private init; }

    public Money UnitPrice { get; private init; } = null!;
    public Money Total { get; private init; } = null!;

    public OrderLineState Status {get; private set;}

    private List<string> _serialNumbers = [];
    public IReadOnlyList<string> SerialNumbers { get{ return _serialNumbers.AsReadOnly(); } private set{_serialNumbers = value is null ?[] : value.ToList();} }

    public Result<Success> SetSerialNumbers(List<string> serialNumbers)
    {
        if (serialNumbers.Count != Quantity)
        {
            return DomainErrors.Orders.SerialNumbersOutOfRange;
        }

        _serialNumbers = serialNumbers;

        return Result.Success;
    }




    private static Result<Success> ValidateQuantity(short quantity)
    {
        if (ValHelper.IsOutOfRange(quantity, OrderItemRules.MinQuantityValue, OrderItemRules.MaxQuantityValue))
        {
            return DomainErrors.Orders.ItemQuantityOutOfRange;
        }
        return Result.Success;
    }

    private static Result<Success> ValidateProductTitle(string title)
    {

        if (string.IsNullOrEmpty(title))
        {
            return DomainErrors.MissingInput;
        }
        return Result.Success;
    }

}
