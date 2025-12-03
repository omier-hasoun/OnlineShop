
namespace Infrastructure.Identity;

public sealed class UserToken : IdentityUserToken<Guid>
{
    public User? UserInfo { get; set; }
}
