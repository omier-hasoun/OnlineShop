namespace Application.Common.Dtos;

public sealed class DomainEventNotification<TDomainEvent>(TDomainEvent domainEvent) : INotification
where TDomainEvent : IDomainEvent
{
    public TDomainEvent DomainEvent { get; }
        = domainEvent;
}

