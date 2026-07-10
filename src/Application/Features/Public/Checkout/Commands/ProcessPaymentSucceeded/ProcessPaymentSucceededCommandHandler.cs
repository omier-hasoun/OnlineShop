using Application.Features.Public.Checkout.Dtos;
using Domain.Common.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Application.Features.Public.Checkout.Commands.ProcessPaymentSucceeded;

internal sealed class ProcessPaymentSucceededCommandHandler(IAppDbContext context, ILogger<ProcessPaymentSucceededCommandHandler> logger, IPaymentGateway gateway) 
: IRequestHandler<ProcessPaymentSucceededCommand>
{
    public async Task Handle(ProcessPaymentSucceededCommand request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.SessionId))
            throw new InvalidOperationException("SessionId cannot be null or empty.");

        PaymentDetailsDto details = await gateway.GetPaymentDetailsAsync(request.SessionId, ct);

        var orderId = new OrderId(Convert.ToInt64(details.OrderId));

        var order = await context.Orders.FindAsync(orderId);

        if (order is null)
        {
            logger.LogCritical(
                "Error by session Order {OrderId} referenced by payment provider does not exist. payment id = {PaymentId}, session id = {SessionId}",
                orderId,
                details.PaymentId,
                request.SessionId);

            throw new InvalidOperationException();
        }

        switch (details.PaymentStatus)
        {
            case PaymentState.Paid:

                await HandlePaidOrder(details, order, ct);

                break;

                // later when integrating with more payment methods we need to handle other payment statuses

            default:
                logger.LogCritical("Unexpected payment status \'{Status}\' for order = {OrderId}, payment id = {PaymentId}, session id = {SessionId}",
                    details.PaymentStatus, orderId, details.PaymentId, request.SessionId);

                throw new InvalidOperationException();
        }

    }

    private async Task HandlePaidOrder(PaymentDetailsDto details, Order order, CancellationToken ct)
    {
        var emailResult = EmailAddress.Create(details.Email);

        if (emailResult.Failed)
        {
            LogError(details.PaymentId, order.Id, ApplicationErrors.Validation.InvalidEmail);
            order.MarkAsRefundRequired();

        }
        else
        {
            order.SetEmailAddress(emailResult.Value);
            var markPaidResult = order.MarkAsConfirmed(details.BillingAddress, details.ShippingAddress, Money.Create(details.TaxAmount)); 

            if (markPaidResult.Failed)
            {
                LogError(details.PaymentId, order.Id, markPaidResult.TopError);
                order.MarkAsRefundRequired();

            }
        }

        await context.SaveAsync(ct);
    }


    private void LogError(string paymentId, OrderId orderId, Error error)
    {
        logger.LogError("order failed to be marked as paid and it will be refunded. provider payment id = {PaymentId}, order id = {OrderId}, error code = {ErrorCode}", paymentId, orderId, error.Code);
    }
}
