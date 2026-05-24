
namespace Domain.Common.Abstractions;

public abstract class AggregateRoot<TId> : BaseEntity<TId>, IAggregateRoot
where TId : struct, IEquatable<TId>
{
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();


    private readonly List<IDomainEvent> _domainEvents = [];

    protected AggregateRoot(TId Id) : base(Id)
    {
    }
    protected AggregateRoot() : base()
    {
        
    }

    public void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        if (domainEvent is null)
            return;

        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

}
