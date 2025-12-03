
namespace Infrastructure.Data.Configs.IdentityConfigs;

public sealed class UserLoginProviderConfig : IEntityTypeConfiguration<UserLoginProvider>
{
    public void Configure(EntityTypeBuilder<UserLoginProvider> builder)
    {
        builder.HasKey(x => new { x.LoginProvider, x.ProviderKey });

        builder.Property(x => x.LoginProvider)
               .ValueGeneratedNever();

        builder.Property(x => x.ProviderKey)
               .ValueGeneratedNever()
               .HasColumnType("VARCHAR(120)");

        builder.Property(x => x.ProviderDisplayName)
               .HasColumnType("VARCHAR(20)")
               .IsRequired();

        builder.HasOne(x => x.UserInfo)
               .WithOne(x => x.LinkedLoginProvider)
               .HasForeignKey<UserLoginProvider>(x => x.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.ProviderDisplayName)
               .HasDatabaseName("IX_ProviderDisplayName");

        builder.ToTable("UserLoginProviders");

    }
}
