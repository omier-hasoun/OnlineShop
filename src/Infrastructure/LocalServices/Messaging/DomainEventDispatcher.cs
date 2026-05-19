using Application.Common.Dtos;
using MediatR;

namespace Infrastructure.LocalServices.Messaging;

internal sealed class DomainEventDispatcher(IPublisher publisher)
: IDomainEventDispatcher
{
    public async Task DispatchAsync(
    IReadOnlyCollection<DomainEvent> domainEvents,
    CancellationToken ct = default)
    {

        foreach (var domainEvent in domainEvents)
        { 
            var notification = CreateNotification(domainEvent);

            await publisher.Publish(notification, ct);

        }

    }
    private static INotification CreateNotification(DomainEvent domainEvent)
    {

        var notificationType = typeof(DomainEventNotification<>)
                                            .MakeGenericType(domainEvent.GetType());

        return (INotification)Activator.CreateInstance(notificationType, domainEvent)!;
    }
}
