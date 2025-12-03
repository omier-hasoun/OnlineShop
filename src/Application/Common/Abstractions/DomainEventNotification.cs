

namespace Application.Common.Abstractions;

public class DomainEventNotification<TEvent> : INotification
where TEvent : IDomainEvent
{
    public TEvent DomainEvent { get; }

    public DomainEventNotification(TEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }
}
