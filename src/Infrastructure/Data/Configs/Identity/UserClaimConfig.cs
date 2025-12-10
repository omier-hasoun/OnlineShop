
namespace Infrastructure.Data.Configs.Identity;

public sealed class UserClaimConfig : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.ClaimValue)
               .HasColumnType("NVARCHAR(64)")
               .IsRequired();

        builder.Property(x => x.ClaimType)
               .HasColumnType("NVARCHAR(64)")
               .IsRequired();

        builder.HasOne<User>()
               .WithMany(x => x.Claims)
               .HasForeignKey(x => x.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        builder.UseTpcMappingStrategy();

        builder.ToTable("UserClaims");
    }
}
