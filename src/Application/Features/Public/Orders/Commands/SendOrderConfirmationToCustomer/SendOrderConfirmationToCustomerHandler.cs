
using Domain.Orders.Events;
using Microsoft.Extensions.Logging;

namespace Application.Features.Public.Orders.Commands.SendOrderConfirmationToCustomer;

internal sealed class SendOrderConfirmationToCustomerHandler(IEmailService emailService, ILogger<SendOrderConfirmationToCustomerHandler> logger) : INotificationHandler<DomainEventNotification<OrderPaidSuccessfully>>
{
    public async Task Handle(DomainEventNotification<OrderPaidSuccessfully> notification, CancellationToken ct)
    {
        var dm = notification.DomainEvent;

        var subject = dm.BillingAddress.FullName + ", Order confirmed";

        var body = @$"
Hello {dm.BillingAddress.FullName},

Your order has been confirmed.
Thank you for ordering from alternate.

order details:

order id: {dm.orderId}

billing address : 
{dm.BillingAddress}

shipping address : 
{dm.ShippingAddress}



order subtotal amount: {dm.SubTotal}
order shipping cost: {dm.ShippingCost}
order total amount: {dm.Total}";

        var request = new EmailMessageRequest("Omier Hasoun", emailService.NoReplyInfoEmail, dm.Email.ToString(), subject, body);

        try
        {
            await emailService.SendEmailAsync(request);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }
    }
}
