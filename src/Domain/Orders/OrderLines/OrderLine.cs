
namespace Domain.Orders.OrderItems;

public sealed class OrderLine : BaseEntity<OrderLineId>
{
    private OrderLine(OrderLineId id, OrderId orderId, ProductId productId, short quantity, string productTitleSnapshot,
        Money unitPrice, Money totalPrice, Money taxAmount, OrderLineStatus status)
        : base(id)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        ProductTitleSnapshot = productTitleSnapshot;
        UnitPrice = unitPrice;
        TotalPrice = totalPrice;
        TaxAmount = taxAmount;
        Status = status;
    }

    private static decimal CalculateTotalPrice(decimal unitPrice, short quantity)
        => unitPrice * quantity;
    
    internal static Result<OrderLine> Create(OrderLineId id, OrderId orderId, ProductId productId, string productTitleSnapshot,
        short quantity, Money unitPrice, Money taxAmount)
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

        var totalPriceResult = Money.Create(CalculateTotalPrice(unitPrice.Value, quantity));

        //if (totalPriceResult.Failed)
        //    return totalPriceResult.Errors;



        return new OrderLine(id, orderId, productId, quantity, productTitleSnapshot, unitPrice, totalPriceResult, taxAmount, OrderLineStatus.Pending);
    }



    public OrderId OrderId { get; private init; }
    public ProductId ProductId { get; private init; }

    public string ProductTitleSnapshot { get; private init; }
    public short Quantity { get; private init; }

    public Money UnitPrice { get; private init; } = null!;
    public Money TotalPrice { get; private init; } = null!;
    public Money TaxAmount { get; private init; } = null!;


    public OrderLineStatus Status {get; private set;}

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
