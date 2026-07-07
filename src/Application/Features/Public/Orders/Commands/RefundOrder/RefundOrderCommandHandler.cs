
namespace Application.Features.Public.Orders.Commands.RefundOrder;

internal sealed class RefundOrderCommandHandler(IAppDbContext db, IPaymentGateway payment) : IRequestHandler<RefundOrderCommand>
{
    public async Task Handle(RefundOrderCommand request, CancellationToken ct)
    {
        if (request.OrderIds is null || request.OrderIds.Count == 0)
            return;

        var orders = await db.Orders.Where(x => request.OrderIds.Contains(x.Id))
                                    .ToListAsync(ct);

        foreach (var order in orders)
        {
            if (order.Status == OrderState.RefundRequired)
            {

                order.MarkAsRefunded();
                await payment.RefundAsync(order.ProviderReferenceId!, ct);
            }
        }

        await db.SaveAsync(ct);
    }
}
