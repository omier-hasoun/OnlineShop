
using Application.Features.Public.Checkout.Commands.ProcessCheckoutCompleted;
using Infrastructure.Data.Models;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.BackgroundJobs;

internal sealed class StripeEventProcessingWorker(IServiceProvider provider, TimeProvider time, ILogger<StripeEventProcessingWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(30), time);

        while(await timer.WaitForNextTickAsync(ct))
        {
            using var scope = provider.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var stripeEvents = await db.StripeEvents.AsNoTracking().
                                                Where(x => x.Status == StripeEventState.Pending)
                                               .Take(10)
                                               .ToListAsync(ct);
            if (stripeEvents.Count == 0)
                continue;

            foreach (var e in stripeEvents)
            {

                await UpdateStatus(db, e.Id, StripeEventState.Processing, ct);
                try
                {
                    await mediator.Send(new ProcessCheckoutCompletedCommand(e.StripeSessionId), ct);

                    await UpdateStatus(db, e.Id, StripeEventState.Processed, ct);
                }
                catch
                {
                    logger.LogError("Failed to process stripe event with id = {stripeEventId}", e.StripeEventId);
                    await UpdateStatus(db, e.Id, StripeEventState.Failed, ct);
                }

            }

        }

    }

    private async Task UpdateStatus(AppDbContext db, long id, StripeEventState status, CancellationToken ct)
    {
        DateTime? processedAt = null;
        if(status == StripeEventState.Processed || status == StripeEventState.Failed)
        {
            processedAt = time.GetUtcNow().UtcDateTime;
        }

        await db.StripeEvents
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(
                x =>
                {
                    x.SetProperty(s => s.Status, status);
                    x.SetProperty(s => s.ProcessedAt, processedAt);
                },
                ct);
    }

}
