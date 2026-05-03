

using Domain.Customers;

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
               .HasColumnType("VARCHAR(255)")// password hash length is 69
               .IsRequired();

        builder.Property(x => x.TwoFactorEnabled)
               .IsRequired();

        builder.HasMany(x => x.Roles)
               .WithMany()
               .UsingEntity<IdentityUserRole<Guid>>();

        builder.HasOne(x => x.LinkedLoginProvider)
               .WithOne()
               .HasForeignKey<UserLoginProvider>(x => x.UserId)
               .IsRequired(false);

        builder.HasMany(x => x.Claims)
               .WithOne()
               .HasForeignKey(x => x.UserId)
               .IsRequired(false);

        builder.HasMany(x => x.Tokens)
               .WithOne()
               .HasForeignKey(x => x.UserId)
               .IsRequired();

        builder.HasIndex(x => x.NormalizedEmail)
               .IsUnique()
               .HasDatabaseName("IX_User_NormalizedEmail");

        builder.HasOne<Customer>()
               .WithMany()
               .HasForeignKey(x => x.CustomerId)
               .IsRequired(false);

        builder.ToTable("Users");
    }
}
