
namespace Infrastructure.Data.Configs.IdentityConfigs;

public sealed class UserTokenConfig : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });

        builder.Property(x => x.Value)
               .HasColumnType("VARCHAR(64)")
               .IsRequired();

        builder.Property(x => x.Name)
               .HasColumnType("VARCHAR(64)")
               .IsRequired();

        builder.HasOne(x => x.UserInfo)
               .WithMany(x => x.Tokens)
               .HasForeignKey(x => x.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.LoginProvider });

        builder.ToTable("UserTokens");
    }
}
