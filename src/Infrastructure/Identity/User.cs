
using Domain.Users;

namespace Infrastructure.Identity;

public sealed class User : IdentityUser<UserId>
{
    public ICollection<UserClaim> Claims { get; private set; } = [];
    public UserLoginProvider? LinkedLoginProvider { get; private set;}
    public ICollection<UserToken> Tokens { get;private set; } = [];
    public ICollection<Role> Roles { get; private set; } = [];

    public User? CustomerInfo { get; private set; }

    public User()
    {

    }

}
