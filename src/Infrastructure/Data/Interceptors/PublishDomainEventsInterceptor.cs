
namespace Infrastructure.Data.Interceptors;

internal sealed class PublishDomainEventsInterceptor(IDomainEventDispatcher domainEventDispatcher) : SaveChangesInterceptor
{

    private List<DomainEvent> _emittedEvents = new();

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken ct = default)
    {
        if (eventData.Context is not null)
        {
            _emittedEvents = eventData.Context.ChangeTracker.Entries<IEntity>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    var events = entity.DomainEvents.ToList();
                    entity.ClearDomainEvents();
                    return events;
                })
                .ToList();
        }



        return base.SavingChangesAsync(eventData, result, ct);
    }

    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken ct = default)
    {
        domainEventDispatcher.DispatchAsync(_emittedEvents, ct);

        _emittedEvents.Clear();

        return base.SavedChangesAsync(eventData, result, ct);
    }
}
