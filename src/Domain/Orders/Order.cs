
using Domain.Services.Models;

namespace Domain.Orders;

public sealed class Order : AggregateRoot<OrderId>, IHasCreationTime
{
    private Order()
    {
        
    }

    private Order(OrderId id, Guid? userId, Money totalPrice, EmailAddress email, Money totalTaxAmount,
            Money shippingFees, IReadOnlyList<OrderLine> items, 
            DateTime createdAt)
        : base(id)
    {
        UserId = userId;
        TotalPrice = totalPrice;
        Email = email;
        TotalTaxAmount = totalTaxAmount;
        ShippingFees = shippingFees;
        Items = items;
        CreatedAt = createdAt;
    }

    internal static Result<Order> Create(OrderId id, Guid? userId, Money shippingFees, Money totalTaxAmount, Money totalPrice,
        EmailAddress email, IReadOnlyList<OrderLinePreview> items)
    {
        //var validationResult = Result.ValidateAll(
        //                        () => ValidateOrderItemDetails(items),
        //                        () => id.IsValid()
        //                    );

        //if (validationResult.Failed)
        //    return validationResult.Errors;

        //List<OrderLine> orderItems = new (items.Count);

        //foreach (var item in items)
        //{
        //    var orderItemResult = OrderLine.Create(item.Id, id, item.ProductId, item.ProductTitle,
        //                                           item.Quantity, item.UnitPrice, item.TaxAmount);

        //    if (orderItemResult.Failed)
        //        return orderItemResult.Errors;

        //    orderItems.Add(orderItemResult.Value);
        //}

        //return new Order(id, userId, totalPrice, email, totalTaxAmount, shippingFees, orderItems, DateTime.UtcNow);

        throw new NotImplementedException();
    }
    public Guid? UserId { get; private init; }
    public EmailAddress Email { get; private init; }
    public Address BillingAddress { get; private init; }
    public Money TotalPrice { get; private init; }
    public Money TotalTaxAmount { get; private init; }

    public Money ShippingFees { get; private init; }
    public DateTime CreatedAt { get; set; }


    //private List<Transaction> _payments = [];
    //public IReadOnlyList<Transaction> Payments { get { return _payments; } private set { _payments = value.ToList(); } }

    public IReadOnlyList<OrderLine> Items { get; private init; }


    private List<Shipment> _shipments = [];
    public IReadOnlyList<Shipment> Shipments { get { return _shipments; } private set { _shipments = value.ToList(); } }

    private static Result<Success> ValidateOrderItemDetails(IReadOnlyList<OrderLinePreview> items)
    {
        if (items is null || ValHelper.IsOutOfRange(items.Count, OrderRules.MinOrderItemsCount, OrderRules.MaxOrderItemsCount))
        {
            return DomainErrors.Orders.ItemsOutOfRange;
        }

        return Result.Success;
    }
}
