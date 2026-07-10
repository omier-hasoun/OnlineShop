using Application.Common.Dtos;
using MediatR;

namespace Infrastructure.Services.Messaging;

internal sealed class DomainEventDispatcher(IPublisher publisher)
: IDomainEventDispatcher
{
    private static INotification CreateNotification(IDomainEvent domainEvent)
    {

        var notificationType = typeof(DomainEventNotification<>)
                                            .MakeGenericType(domainEvent.GetType());

        return (INotification)Activator.CreateInstance(notificationType, domainEvent)!;
    }

    public async Task DispatchAsync(IDomainEvent domainEvent, CancellationToken ct = default)
    {
        var notification = CreateNotification(domainEvent);

        await publisher.Publish(notification, ct);
    }
}
