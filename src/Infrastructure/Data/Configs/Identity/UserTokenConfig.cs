
namespace Infrastructure.Data.Configs.Identity;

public sealed class UserTokenConfig : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });

        builder.Property(x => x.Value)
               .HasColumnType("VARCHAR(255)")
               .IsRequired();

        builder.Property(x => x.Name)
               .HasColumnType("VARCHAR(100)")
               .IsRequired();

        
        builder.Property(x => x.LoginProvider)
               .HasColumnType("VARCHAR(255)")
               .IsRequired();

        builder.ToTable("UserTokens");
    }
}
