
using System.Text.Json;
using Infrastructure.Data.Models;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.BackgroundJobs;

internal sealed class OutboxMessagesProcessingWorker(IServiceProvider provider, TimeProvider time, ILogger<OutboxMessagesProcessingWorker> logger) : BackgroundService
{
    private static readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    protected async override Task ExecuteAsync(CancellationToken ct)
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(60), time);

        while (await timer.WaitForNextTickAsync(ct))
        {
            using var scope = provider.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var messages = await db.OutboxMessages.Where(x => x.ProcessedOnUtc == null && x.Error == null)
                                                  .Take(200)
                                                  .ToListAsync(ct);
            if (messages.Count == 0)
            
                continue;
            
            var dispatcher = scope.ServiceProvider.GetRequiredService<IDomainEventDispatcher>();
            foreach (var message in messages)
            {
                try
                {
                    var eventType = Type.GetType(message.Type);

                    if (eventType is null)
                    {
                        message.MarkFailed($"Cannot find type {message.Type}");
                        continue;
                    }

                    var domainEvent = (IDomainEvent?)JsonSerializer.Deserialize(
                        message.Content,
                        eventType,
                        _serializerOptions);

                    if (domainEvent is null)
                    {
                        message.MarkFailed("Deserialization failed");
                        continue;
                    }

                    await dispatcher.DispatchAsync(domainEvent, ct);

                    message.MarkProcessed();
                }
                catch (Exception ex)
                {
                    logger.LogError(
                        "Failed processing outbox message {Id}",
                        message.Id);

                    message.MarkFailed(ex.Message);
                }
            }

            await db.SaveChangesAsync(ct);



        }
    }
}
