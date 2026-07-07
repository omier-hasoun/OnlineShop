
using Application.Common.Dtos;
using Domain.Orders.Events;

namespace Application.Features.Public.Orders.Commands.SendOrderConfirmationToCustomer;

internal sealed class SendOrderConfirmationToCustomerHandler(INotificationService notifier, ApplicationSettings settings) : INotificationHandler<DomainEventNotification<OrderConfirmed>>
{
    public async Task Handle(DomainEventNotification<OrderConfirmed> notification, CancellationToken ct)
    {
        var dm = notification.DomainEvent;

        var subject = dm.BillingAddress.FullName + ", Order confirmed";

        var body = @$"
Hello {dm.BillingAddress.FullName},

Your order has been confirmed.
Thank you for ordering from {settings.BusinessName}.

order details:

order id: {dm.orderId}

billing address : 
{dm.BillingAddress}

shipping address : 
{dm.ShippingAddress}



order subtotal amount: {dm.SubTotal}
order shipping cost: {dm.ShippingCost}
order total amount: {dm.Total}";

        var request = new NotificationRequest("om@gmail.com", dm.Email.ToString(), subject, body);
        await notifier.NotifyAsync(request);
    }
}
