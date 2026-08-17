

using Application.Entities;
 

namespace Infrastructure.Data.Configs.Identity;

internal sealed class AppUserConfig : BaseEntityConfig<AppUser>
{
    public override void Configure(EntityTypeBuilder<AppUser> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id)
               .IsClustered();

        builder.Property(x => x.Id)
               .ValueGeneratedNever();

        builder.Property(x => x.Email)
               .HasColumnType("VARCHAR(254)")
               .IsRequired();

        builder.Property(x => x.NormalizedEmail)
               .HasColumnType("VARCHAR(254)")
               .IsRequired();

        builder.Property(x => x.UserName)
               .HasColumnType("VARCHAR(254)")
               .IsRequired();

        builder.Property(x => x.NormalizedUserName)
               .HasColumnType("VARCHAR(254)")
               .IsRequired();


        builder.Property(x => x.PhoneNumber)
               .HasColumnType("VARCHAR(30)")
               .IsRequired(false);

        builder.Property(x => x.ConcurrencyStamp)
               .HasColumnType("VARCHAR(50)")
               .IsRequired();

        builder.Property(x => x.SecurityStamp)
               .HasColumnType("VARCHAR(50)")
               .IsRequired();

        builder.Property(x => x.LockoutEnabled)
               .IsRequired();

        builder.Property(x => x.LockoutEnd)
               .IsRequired(false);

        builder.Property(x => x.AccessFailedCount)
               .IsRequired();

        builder.Property(x => x.PasswordHash)
               .HasColumnType("VARCHAR(255)")
               .IsRequired();

        builder.Property(x => x.TwoFactorEnabled)
               .IsRequired();

        builder.HasMany(x => x.Claims)
               .WithOne()
               .HasForeignKey(x => x.UserId)
               .IsRequired();

        builder.HasMany(x => x.Tokens)
               .WithOne()
               .HasForeignKey(x => x.UserId)
               .IsRequired();

        builder.HasIndex(x => x.NormalizedEmail)
               .IsUnique()
               .HasDatabaseName("IX_Users_NormalizedEmail");

        builder.HasIndex(x => x.NormalizedUserName)
               .IsUnique()
               .HasDatabaseName("IX_Users_NormalizedUserName");

        builder.ToTable("Users");
    }
}
