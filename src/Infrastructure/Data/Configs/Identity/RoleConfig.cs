
namespace Infrastructure.Data.Configs.Identity;

public sealed class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever();

        builder.Property(x => x.Name)
               .HasColumnType("VARCHAR(50)")
               .IsRequired();

        builder.Property(x => x.NormalizedName)
               .HasColumnType("VARCHAR(50)")
               .IsRequired();

        builder.Property(x => x.ConcurrencyStamp)
               .HasConversion<Guid>(x => Guid.Parse(x!), x => x.ToString())
               .IsRequired();

        builder.ToTable("Roles");
    }
}
