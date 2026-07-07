using Application.Features.Public.Orders.Commands.RefundOrder;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.BackgroundJobs;

internal sealed class OrderRefundProcessingWorker(TimeProvider time, IServiceProvider provider) : BackgroundService
{
    protected async override Task ExecuteAsync(CancellationToken ct)
    {
        var timer = new PeriodicTimer(TimeSpan.FromMinutes(15), time);

        while (await timer.WaitForNextTickAsync(ct))
        {
            using var scope = provider.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var orderIds = await db.Orders.AsNoTracking()
                                   .Where(x => x.Status == Domain.Orders.OrderState.RefundRequired)
                                   .Select(x => x.Id)
                                   .Take(10)
                                   .ToListAsync(ct);

            await mediator.Send(new RefundOrderCommand(orderIds), ct);
        }

    }
}
