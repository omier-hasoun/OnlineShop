namespace Infrastructure.Identity;

public sealed class Role : IdentityRole<Guid>
{
    public ICollection<RoleClaim> RoleClaims { get; set; } = [];

    public Role()
    {

    }
    public Role(string roleName) : base(roleName)
    {
        if (Id == default)
            Id = Guid.CreateVersion7();
    }
}
