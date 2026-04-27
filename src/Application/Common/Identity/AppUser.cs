using Domain.Customers;

namespace Application.Common.Identity;

public sealed class AppUser : IdentityUser<Guid>, IEntity
{
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    private List<IDomainEvent> _domainEvents = [];
    public ICollection<UserClaim> Claims { get; private set; } = [];
    public UserLoginProvider? LinkedLoginProvider { get; private set; } = null!;
    public ICollection<UserToken> Tokens { get; private set; } = [];
    public ICollection<Role> Roles { get; private set; } = [];

    public CustomerId UserId => Id;


    public AppUser()
    {
        if(Id == default)
        {
            Id = Guid.CreateVersion7();
        }
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

