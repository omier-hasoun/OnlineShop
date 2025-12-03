namespace Infrastructure.Identity;

public sealed class RoleClaim : IdentityRoleClaim<Guid>
{
    public Role? Role { get; set; }

    public RoleClaim()
    {

    }
}
