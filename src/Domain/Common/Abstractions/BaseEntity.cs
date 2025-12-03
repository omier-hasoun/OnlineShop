
namespace Domain.Common.Abstractions;

public abstract class BaseEntity
{
    public Guid Id
    {
        get => field;
        init =>
        field = value == Guid.Empty ? Guid.CreateVersion7() : value;
    }
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();


    private readonly List<IDomainEvent> _domainEvents = [];

    protected BaseEntity()
    {

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
