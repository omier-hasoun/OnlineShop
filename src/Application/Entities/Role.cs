namespace Application.Entities;

public sealed class Role : IdentityRole<Guid>
{
    //public ICollection<RoleClaim> RoleClaims { get; set; } = [];

    public Role()
    {

    }
    public Role(string roleName)
    {
        if (Id == default)
            Id = Guid.CreateVersion7();

        Name = roleName;
    }
}
