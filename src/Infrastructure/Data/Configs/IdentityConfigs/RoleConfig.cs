
namespace Infrastructure.Data.Configs.IdentityConfigs;

public sealed class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever();

        builder.Property(x => x.Name)
               .HasColumnType("varchar(32)");

        builder.Property(x => x.ConcurrencyStamp)
               .HasColumnType("varchar(32)");

        builder.ToTable("Roles");
    }
}
