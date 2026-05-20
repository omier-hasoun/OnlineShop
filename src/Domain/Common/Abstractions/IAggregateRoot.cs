

namespace Domain.Common.Abstractions;

public interface IAggregateRoot : IEntity
{
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }
    void AddDomainEvent(DomainEvent domainEvent);
    void RemoveDomainEvent(DomainEvent domainEvent);
    void ClearDomainEvents();
}
