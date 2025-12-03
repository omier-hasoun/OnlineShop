namespace Infrastructure.Identity;

public sealed class UserLoginProvider : IdentityUserLogin<Guid>
{
    public User? UserInfo { get; set; }

    public UserLoginProvider()
    {

    }
}
