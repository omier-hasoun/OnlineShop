
namespace Infrastructure.Data.Configs.IdentityConfigs;

public sealed class UserClaimConfig : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedOnAdd();

        builder.HasOne<User>()
               .WithMany(x => x.Claims)
               .HasForeignKey(x => x.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("UserClaims");
    }
}
