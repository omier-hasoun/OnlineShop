
namespace Infrastructure.Data.Configs.Identity;

public sealed class UserLoginProviderConfig : IEntityTypeConfiguration<UserLoginProvider>
{
    public void Configure(EntityTypeBuilder<UserLoginProvider> builder)
    {
        builder.HasKey(x => new { x.LoginProvider, x.ProviderKey });

        builder.Property(x => x.LoginProvider)
                .HasColumnType("VARCHAR(255)")
               .ValueGeneratedNever();

        builder.Property(x => x.ProviderKey)
               .ValueGeneratedNever()
               .HasColumnType("VARCHAR(255)");

        builder.Property(x => x.ProviderDisplayName)
               .HasColumnType("VARCHAR(50)")
               .IsRequired();

        builder.ToTable("UserLoginProviders");
    }
}
