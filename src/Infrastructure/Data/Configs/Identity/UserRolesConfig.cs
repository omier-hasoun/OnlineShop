

namespace Infrastructure.Data.Configs.Identity;

public sealed class UserRolesConfig : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.UserId });

        builder.UseTpcMappingStrategy();

        builder.ToTable("UserRoles");
    }
}
