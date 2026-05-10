
using Application.Entities;

namespace Infrastructure.Data.Configs.Identity;

public sealed class UserClaimConfig : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.ClaimValue)
               .HasColumnType("NVARCHAR(255)")
               .IsRequired();

        builder.Property(x => x.ClaimType)
               .HasColumnType("VARCHAR(100)")
               .IsRequired();

        builder.ToTable("UserClaims");
    }
}
