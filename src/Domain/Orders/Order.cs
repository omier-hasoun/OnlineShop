
using Domain.Orders.Events;

namespace Domain.Orders;

public sealed class Order : AggregateRoot<OrderId>, IHasCreationTime
{
    private Order()
    {
        
    }

    private Order(OrderId id, Guid? userId, GuestAccountId? guestId, Money subTotal, Money total, 
            Money shippingCost, Money taxAmount, IReadOnlyList<OrderLine> lines, string? providerPaymentId, OrderState status,
            DateTime createdAt)
        : base(id)
    {
        UserId = userId;
        SubTotal = subTotal;
        Total = total;
        GuestId = guestId;
        ShippingCost = shippingCost;
        TaxAmount = taxAmount;
        Lines = lines;
        ProviderReferenceId = providerPaymentId;
        Status = status;
        CreatedAt = createdAt;
    }

    internal static Result<Order> Create(OrderId id, Guid? userId, GuestAccountId? guestId, Money subTotal, Money total,
    Money shippingCost, string? providerPaymentId, IReadOnlyList<OrderLine> lines)
    {
        var validationResult = Result.ValidateAll(
                                () => ValidateOrderLines(lines),
                                () => id.IsValid());

        if (validationResult.Failed)
            return validationResult.Errors;

        var taxAmount = Money.Zero;
        return new Order(id, userId, guestId, subTotal, total, shippingCost, taxAmount, lines, providerPaymentId, OrderState.Pending, DateTime.UtcNow);
    }
    public GuestAccountId? GuestId { get; private init; }
    public Guid? UserId { get; private init; }
    public EmailAddress? Email { get; private set; }
    public AddressDetails? BillingAddress { get; private set; }
    public AddressDetails? ShippingAddress { get; private set; }
    public OrderState Status { get; private set; }
    public Money Total { get; private set; }
    public Money TaxAmount { get; private set; }
    public Money SubTotal { get; private set; }
    public Money ShippingCost { get; private set; }

    public string? ProviderReferenceId { get; private set; }

    public DateTime CreatedAt { get; set; }

    public IReadOnlyList<OrderLine> Lines { get; private init; }

    private List<Shipment> _shipments = [];
    public IReadOnlyList<Shipment> Shipments { get { return _shipments; } private set { _shipments = value.ToList(); } }

    public Result<Updated> SetProviderPaymentId(string id)
    {
        if (Status != OrderState.Pending)
        {
            return DomainErrors.Locked;
        }
        ProviderReferenceId = id;
        return Result.Updated;
    }

    public Result<Updated> MarkAsConfirmed(AddressDetails billingAddress, AddressDetails shippingAddress, EmailAddress email, Money taxAmount)
    {
        if (!CanTransitionTo(OrderState.Confirmed))
        {
            return DomainErrors.InvalidStateTransition;
        }

        if (string.IsNullOrEmpty(ProviderReferenceId))
        {
            return DomainErrors.ProviderReferenceIdMissing;
        }

        var validationResult = Result.ValidateAll(
                                () => ValidateEmail(email),
                                () => ValidateBillingAddress(billingAddress),
                                () => ValidateShippingAddress(shippingAddress),
                                () => ValidateTaxAmount(taxAmount));

        if (validationResult.Failed)
            return validationResult.Errors;

        this.Status = OrderState.Confirmed;
        this.Email = email;
        this.BillingAddress = billingAddress;
        this.ShippingAddress = shippingAddress;

        this.TaxAmount = taxAmount;
        this.Total = Money.Create(SubTotal.Value + taxAmount.Value + ShippingCost.Value);
        RaiseDomainEvent(new OrderConfirmed(Id, Email, Total, SubTotal, ShippingCost, ShippingAddress, BillingAddress));

        return Result.Updated;
    }

    public Result<Updated> MarkAsRefunded()
    {
        if (!CanTransitionTo(OrderState.Refunded))
        {
            return DomainErrors.InvalidStateTransition;
        }

        this.Status = OrderState.Refunded;
       
        return Result.Updated;
    }

    public void MarkAsRefundRequired()
    {
        this.Status = OrderState.RefundRequired;
    }

    private bool CanTransitionTo(OrderState newStatus)
    {
        return (Status, newStatus) switch
        {
            (OrderState.Pending, OrderState.Confirmed) => true,
            (OrderState.Confirmed, OrderState.RefundRequired) => true,
            (OrderState.Confirmed, OrderState.Processing) => true,
            (OrderState.Processing, OrderState.Delivered) => true,
            (OrderState.Processing, OrderState.RefundRequired) => true,
            (OrderState.Delivered, OrderState.RefundRequired) => true,
            (OrderState.RefundRequired, OrderState.Refunded) => true,

            _ => false
        };

    }
    private static Result<Success> ValidateOrderLines(IReadOnlyList<OrderLine> lines)
    {
        if (lines is null || ValHelper.IsOutOfRange(lines.Count, OrderRules.MinOrderLinesNumber, OrderRules.MaxOrderLinesNumber))
        {
            return DomainErrors.Orders.ItemsNumberLimitExceeded;
        }

        return Result.Success;
    }

    private static Result<Success> ValidateEmail(EmailAddress email) => 
      email != null ? Result.Success : DomainErrors.Orders.EmailInvalid;
    private static Result<Success> ValidateBillingAddress(AddressDetails billingAddress) =>
  billingAddress != null ? Result.Success : DomainErrors.Orders.BillingAddressInvalid;
    private static Result<Success> ValidateShippingAddress(AddressDetails shippingAddress) =>
  shippingAddress != null ? Result.Success : DomainErrors.Orders.ShippingAddressInvalid;

    private static Result<Success> ValidateTaxAmount(Money taxAmount) =>
taxAmount != null ? Result.Success : DomainErrors.Orders.TaxAmountInvalid;
}
