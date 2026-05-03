
using Domain.Common.ValueObjects;

namespace Domain.Orders;

public sealed class Order : AggregateRoot<OrderId>, IHasCreationTime
{

    private Order(OrderId id, CustomerId customerId, Money totalItemsPrice, Money shippingFees, DateTime createdAt)
        : base(id)
    {
        CustomerId = customerId;
        TotalItemsPrice = totalItemsPrice;
        ShippingFees = shippingFees;
        CreatedAt = createdAt;
    }
    private static decimal CalculateTotalItemsPrice(IReadOnlyList<OrderItem> items)
    {
        if(items == null)
            return 0;

        return items.Sum(item => item.TotalPrice.Value);
    }
    //public static Result<Order> Create(OrderId id, UserId customerId, decimal shippingFees)
    //{

    //    var result = Result.ValidateAll(
    //        //() => ValidateOrderItems(items),
    //        () => ValidateShippingFees(shippingFees),
    //        //() => ValidateTotalItemsPrice(totalItemsPrice)
    //        );

    //    if (result.Failed)
    //    {
    //        return result.Errors;
    //    }


    //    return new Order(id, customerId, totalItemsPrice, shippingFees, items, TimeService.UtcNow);
    //}
    public CustomerId CustomerId { get; private init; }
    public Money TotalItemsPrice { get; private set; }
    public Money ShippingFees { get; private set; }
    public DateTime CreatedAt { get; set; }


    private List<OrderPayment> _payments = [];
    public IReadOnlyList<OrderPayment> Payments { get { return _payments; } private set { _payments = value.ToList(); } }


    private List<OrderItem> _items = [];
    public IReadOnlyList<OrderItem> Items { get { return _items; } private set { _items = value.ToList(); } }


    private List<Shipment> _shipments = [];
    public IReadOnlyList<Shipment> Shipments { get { return _shipments; } private set { _shipments = value.ToList(); } }

    private static Result<Success> ValidateShippingFees(decimal shippingFees)
    {
        if(ValHelper.IsOutOfRange(shippingFees, OrderRules.MinShippingFeesValue, OrderRules.MaxShippingFeesValue))
        {
            return DomainErrors.Orders.ShippingFeesOutOfRange;
        }

        return Result.Success;
    }
    private static Result<Success> ValidateTotalItemsPrice(decimal totalItemsPrice)
    {
        if (ValHelper.IsOutOfRange(totalItemsPrice, OrderRules.MinTotalItemsPriceValue, OrderRules.MaxTotalItemsPriceValue))
        {
            return DomainErrors.Orders.TotalItemsPriceOutOfRange;
        }
        return Result.Success;
    }
    private static Result<Success> ValidateOrderItems(IReadOnlyList<OrderItem> orderItems)
    {
        if (orderItems is null || ValHelper.IsOutOfRange(orderItems.Count, OrderRules.MinOrderItemsCount, OrderRules.MaxOrderItemsCount))
        {
            return DomainErrors.Orders.OrderItemsNumberOutOfRange;
        }
        return Result.Success;
    }
}
