using Domain.Customers;

namespace Application.Common.Identity;

public sealed class AppUser : IdentityUser<Guid>
{
    public ICollection<UserClaim> Claims { get; private set; } = [];
    public UserLoginProvider LinkedLoginProvider { get; private set; } = null!;
    public ICollection<UserToken> Tokens { get; private set; } = [];
    public ICollection<Role> Roles { get; private set; } = [];

    public override string? UserName { get => base.Email; set => base.Email = value; }
    public override string? NormalizedUserName { get => base.NormalizedEmail; set => base.NormalizedEmail = value; }


    public UserId UserId => Id;


    public AppUser()
    {

    }

}
