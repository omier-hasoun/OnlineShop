
namespace Domain.Orders.Events;

public sealed record OrderConfirmed(
    OrderId orderId,
    EmailAddress Email,
    Money Total,
    Money SubTotal,
    Money ShippingCost,
    AddressDetails ShippingAddress,
    AddressDetails BillingAddress
) : IDomainEvent;
