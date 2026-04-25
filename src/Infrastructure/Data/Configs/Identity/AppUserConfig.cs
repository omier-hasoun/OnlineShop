

namespace Infrastructure.Data.Configs.Identity;

public sealed class AppUserConfig : BaseEntityConfig<AppUser>
{
    public override void Configure(EntityTypeBuilder<AppUser> builder)
    {
        base.Configure(builder);

        builder.Ignore(x => x.UserId);// UserId is readonly, return the Guid Id value its just a strongly type UserId for the domain

        builder.HasKey(x => x.Id);

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
               .HasConversion<Guid>(x => Guid.Parse(x!), x => x.ToString())
               .IsRequired();

        builder.Property(x => x.SecurityStamp)
               .HasConversion<Guid>(x => Guid.Parse(x!), x => x.ToString())
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

        builder.ToTable("Users");
    }
}
