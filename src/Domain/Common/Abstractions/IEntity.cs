
namespace Domain.Common.Abstractions;

public interface IEntity
{
    public IReadOnlyCollection<DomainEvent> DomainEvents { get;}
    public void AddDomainEvent(DomainEvent domainEvent);
    public void RemoveDomainEvent(DomainEvent domainEvent);

    public void ClearDomainEvents();
}
