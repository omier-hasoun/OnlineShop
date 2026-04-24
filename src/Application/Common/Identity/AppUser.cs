using Domain.Customers;

namespace Application.Common.Identity;

public sealed class AppUser : IdentityUser<Guid>, IEntity
{
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    private List<IDomainEvent> _domainEvents;
    public ICollection<UserClaim> Claims { get; private set; } = [];
    public UserLoginProvider? LinkedLoginProvider { get; private set; } = null!;
    public ICollection<UserToken> Tokens { get; private set; } = [];
    public ICollection<Role> Roles { get; private set; } = [];

    public override string? UserName { get => base.Email; set => base.Email = value; }
    public override string? NormalizedUserName { get => base.NormalizedEmail; set => base.NormalizedEmail = value; }


    public UserId UserId => Id;


    public AppUser()
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

