
using Application.Common.Dtos;
using MediatR;

namespace Infrastructure.Data.Interceptors;

internal sealed class EventsPublisherSaveChangesInterceptor(IDomainEventDispatcher dispatcher) : SaveChangesInterceptor
{
    List<IDomainEvent>? _domainEvents = null;
    public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken ct = default)
    {
        if (eventData.Context is not null)
        {


            _domainEvents = eventData.Context.ChangeTracker.Entries<IAggregateRoot>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    var events = entity.DomainEvents.ToList();
                    entity.ClearDomainEvents();
                    return events;
                })
                .ToList();
        }

        return await base.SavingChangesAsync(eventData, result, ct);
    }

    public async override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken ct = default)
    {
        if (_domainEvents != null)
            await dispatcher.DispatchAsync(_domainEvents, ct);

        return await base.SavedChangesAsync(eventData, result, ct);
    }
}
