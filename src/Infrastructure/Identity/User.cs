
namespace Infrastructure.Identity;

public sealed class User : IdentityUser<Guid>
{
    public ICollection<UserClaim> Claims { get; set; } = [];
    public UserLoginProvider? LinkedLoginProvider { get; set; }
    public ICollection<UserToken> Tokens { get; set; } = [];
    public ICollection<Role> Roles { get; set; } = [];


    public User()
    {
        if (Id == default)
            Id = Guid.CreateVersion7();
    }

}
