
using System.Runtime.CompilerServices;

namespace Infrastructure.Data.Configs.IdentityConfigs;

public sealed class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever();

        builder.Property(x => x.Email)
               .HasColumnType("VARCHAR(256)")
               .IsRequired();

        builder.Property(x => x.NormalizedEmail)
               .HasColumnType("VARCHAR(256)") // max length for an email is 254 chars so 256 is enough for emails
               .IsRequired();

        builder.Property(x => x.ConcurrencyStamp)
               .HasColumnType("CHAR(36)")
               .IsRequired();

        builder.Property(x => x.SecurityStamp)
               .HasColumnType("CHAR(36)")
               .IsRequired();

        builder.Property(x => x.LockoutEnabled)
               .IsRequired();

        builder.Property(x => x.LockoutEnd)
               .IsRequired(false);

        builder.Property(x => x.AccessFailedCount)
               .IsRequired();

        builder.Property(x => x.PasswordHash)
               .HasColumnType("VARCHAR(128)")// password hash length is 69 chars!
               .IsRequired();

        builder.Property(x => x.UserName)
                .HasColumnType("VARCHAR(128)")
                .IsRequired();

        builder.Property(x => x.NormalizedUserName)
               .HasColumnType("VARCHAR(128)")//  100 in case future encrypt but the actual length should not exceed 18
               .IsRequired();

        builder.Property(x => x.TwoFactorEnabled)
               .IsRequired();

        builder.HasMany(x => x.Roles)
               .WithMany()
               .UsingEntity<UserRoles>();

        builder.Ignore(x => x.PhoneNumber);
        builder.Ignore(x => x.PhoneNumberConfirmed);

        builder.HasIndex(x => x.NormalizedUserName).HasDatabaseName("IX_NormalizedUserName");
        builder.HasIndex(x => x.NormalizedEmail).HasDatabaseName("IX_NormalizedEmail");

        builder.ToTable("Users");
    }
}
