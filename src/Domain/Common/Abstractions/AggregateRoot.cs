
namespace Domain.Common.Abstractions;

public abstract class AggregateRoot<TId> : BaseEntity<TId>, IAggregateRoot
where TId : struct, IEquatable<TId>
{
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();


    private readonly List<DomainEvent> _domainEvents = [];

    protected AggregateRoot(TId Id) : base(Id)
    {
    }
    protected AggregateRoot() : base()
    {
        
    }

    public void AddDomainEvent(DomainEvent domainEvent)
    {
        if (domainEvent is null)
            return;

        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
