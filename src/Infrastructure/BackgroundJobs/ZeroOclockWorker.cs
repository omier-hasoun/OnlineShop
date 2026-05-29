

using Application.Common.Configurations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.BackgroundJobs;

internal sealed class ZeroOclockWorker(IEnumerable<IZeroOclockService> services, TimeProvider time, ILogger<ZeroOclockWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        await RunJob();// first time execution at startup

        var initialDelay = GetRemainingTimeUntilZeroOclock();

        await Task.Delay(initialDelay, ct);

        // First execution at exactly 00:00 UTC
        await RunJob();

        using var timer = new PeriodicTimer(TimeSpan.FromDays(1), time);

        while (await timer.WaitForNextTickAsync(ct))
        {
            await RunJob();
        }
    }

    private TimeSpan GetRemainingTimeUntilZeroOclock()
    {
        var now = time.GetUtcNow().UtcDateTime;

        var nextMidnight = now.Date.AddDays(1);

        return nextMidnight - now;
    }

    private async Task RunJob()
    {
        logger.LogInformation(
            "{Worker} running at {Time}",
            nameof(ZeroOclockWorker),
            time.GetUtcNow());

        foreach (var s in services)
        {
            await s.ExecuteAsync();

        }

    }
}
