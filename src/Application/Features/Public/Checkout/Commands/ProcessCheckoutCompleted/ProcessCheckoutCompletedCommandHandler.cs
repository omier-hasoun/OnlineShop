using Application.Features.Public.Checkout.Dtos;
using Domain.Common.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Application.Features.Public.Checkout.Commands.ProcessCheckoutCompleted;

internal sealed class ProcessCheckoutCompletedCommandHandler(IAppDbContext context, ILogger<ProcessCheckoutCompletedCommandHandler> logger, IPaymentGateway gateway) 
: IRequestHandler<ProcessCheckoutCompletedCommand>
{
    private PaymentDetailsDto _details;
    private string _sessionId;
    private OrderId _orderId;
    public async Task Handle(ProcessCheckoutCompletedCommand request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.SessionId))
            throw new InvalidOperationException("SessionId cannot be null or empty.");

        _sessionId = request.SessionId;
        _details = await gateway.GetPaymentDetailsAsync(_sessionId, ct);

        if(_details is null)
        {
            logger.LogError("Couldn't retrieve payement details. Session id: {}", _sessionId);
            throw new InvalidOperationException("Couldn't retrieve payement details.");
        }

        _orderId = new OrderId(Convert.ToInt64(_details.OrderId));

        switch (_details.PaymentStatus)
        {
            case PaymentState.Paid:

                await HandlePaidOrder(ct);

                break;

            case PaymentState.Unpaid:
                await HandleUnpaidOrder(ct);
                break;

            default:
                logger.LogCritical("Unexpected payment status \'{Status}\' for session id = {SessionId}",
                    _details.PaymentStatus, request.SessionId);

                throw new InvalidOperationException();
        }

    }

    private async Task HandlePaidOrder(CancellationToken ct)
    {
        var order = await context.Orders.Include(x => x.Lines).FirstOrDefaultAsync(x => x.Id == _orderId, ct);
        if (order is null)
        {
            LogOrderNotFound();
            return;
        }

        var emailResult = EmailAddress.Create(_details.Email);

        if (emailResult.Failed)
        {
            LogFailedToMarkAsPaid(ApplicationErrors.Validation.InvalidEmail);
            order.MarkAsRefundRequired();

        }
        else
        {
            order.SetEmailAddress(emailResult.Value);
            var markPaidResult = order.MarkAsConfirmed(_details.BillingAddress, _details.ShippingAddress, Money.Create(_details.TaxAmount)); 

            if (markPaidResult.Failed)
            {
                LogFailedToMarkAsPaid(markPaidResult.TopError);
                order.MarkAsRefundRequired();

            }
            else
            {
                var inventories = await context.OrderLines
                                                  .Where(x => x.OrderId == _orderId)
                                                  .Join(context.Products.Include(x => x.Inventory), x => x.ProductId, x => x.Id, (Line, Product) => new { OrderLineQuantity = Line.Quantity, Product.Inventory })
                                                  .ToListAsync(ct);

                foreach (var item in inventories)
                {
                    item.Inventory.TakeQuantityFromReserved(item.OrderLineQuantity);
                }
            }
        }



        await context.SaveAsync(ct);
    }

    private async Task HandleUnpaidOrder(CancellationToken ct)
    {
        var inventories = await context.OrderLines
                                          .Where(x => x.OrderId == _orderId)
                                          .Join(context.Products.Include(x => x.Inventory), x => x.ProductId, x => x.Id, (Line, Product) => new { OrderLineQuantity = Line.Quantity, Product.Inventory })
                                          .ToListAsync(ct);
                                          
        if(inventories.Count == 0)
        {
            LogOrderNotFound();
            return;
        }

        foreach (var item in inventories)
        {
            item.Inventory.CancelQuantityReservation(item.OrderLineQuantity);
        }

        await context.SaveAsync(ct);
        await context.Orders.Where(x => x.Id == _orderId)
                            .ExecuteDeleteAsync(ct);
    }


    private void LogOrderNotFound()
    {
        logger.LogCritical(
            "Error by session Order {OrderId} referenced by payment provider does not exist. payment id = {PaymentId}, session id = {SessionId}",
            _orderId,
            _details.PaymentId,
            _sessionId);
    }

    private void LogFailedToMarkAsPaid(Error error)
    {
        logger.LogError("order failed to be marked as paid and it will be refunded. provider payment id = {PaymentId}, order id = {OrderId}, error code = {ErrorCode}",
            _details.PaymentId,
            _orderId,
            error.Code);
    }
}
