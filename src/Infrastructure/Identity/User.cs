
namespace Infrastructure.Identity;

public sealed class User : IdentityUser<Guid>
{
    public ICollection<UserClaim> Claims { get; private set; } = [];
    public UserLoginProvider? LinkedLoginProvider { get; private set;}
    public ICollection<UserToken> Tokens { get;private set; } = [];
    public ICollection<Role> Roles { get; private set; } = [];

    public CustomerId? CustomerId { get; private set; }
    public Customer? CustomerInfo { get; private set; }

    public User()
    {
        if (Id == default)
            Id = Guid.CreateVersion7();
    }

}
