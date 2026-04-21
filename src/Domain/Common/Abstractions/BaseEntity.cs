
namespace Domain.Common.Abstractions;

public abstract class BaseEntity<TId> : IEntity where TId : struct, IEquatable<TId>
{
    public TId Id { get; init; }
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();


    private readonly List<IDomainEvent> _domainEvents = [];

    protected BaseEntity(TId Id)
    {
        this.Id = Id;
    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        if (domainEvent is null)
            return;

        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

}
