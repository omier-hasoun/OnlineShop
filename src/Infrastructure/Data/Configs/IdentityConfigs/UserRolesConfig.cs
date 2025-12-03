
namespace Infrastructure.Data.Configs.IdentityConfigs;

public sealed class UserRolesConfig : IEntityTypeConfiguration<UserRoles>
{
    public void Configure(EntityTypeBuilder<UserRoles> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.UserId });


        builder.ToTable("UserRoles");
    }
}
