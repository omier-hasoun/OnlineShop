
namespace Infrastructure.Data;

public sealed class AppDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRoles, UserLoginProvider, RoleClaim, UserToken, IdentityUserPasskey<Guid>>, IAppDbContext
{
    public override DbSet<UserClaim> UserClaims { get => base.UserClaims; set => base.UserClaims = value; }

    public override DbSet<RoleClaim> RoleClaims { get => base.RoleClaims; set => base.RoleClaims = value; }

    public override DbSet<UserRoles> UserRoles { get => base.UserRoles; set => base.UserRoles = value; }

    public override DbSet<UserLoginProvider> UserLogins { get => base.UserLogins; set => base.UserLogins = value; }

    public override DbSet<UserToken> UserTokens { get => base.UserTokens; set => base.UserTokens = value; }

    public override DbSet<IdentityUserPasskey<Guid>> UserPasskeys { get => base.UserPasskeys; set => base.UserPasskeys = value; }

    public override DbSet<User> Users { get => base.Users; set => base.Users = value; }

    public override DbSet<Role> Roles { get => base.Roles; set => base.Roles = value; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public AppDbContext()
    {
    }

    public override async Task<int> SaveChangesAsync(CancellationToken ct)
    {

        return await base.SaveChangesAsync(ct);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Softdelete configuration for all Entities implementing ISofDeletable
        ConfigurePropertiesForInterface<ISofDeletable>(builder, (b, type) =>
        {
            b.Property(nameof(ISofDeletable.DeletedAt))
             .IsRequired(false);

            b.Property(nameof(ISofDeletable.DeletedBy))
             .HasColumnType("CHAR(36)")
             .IsRequired(false);
        });

        builder.Entity<IdentityUserPasskey<Guid>>(b =>
        {
            b.HasKey(p => new { p.UserId, p.CredentialId });
            b.Property(p => p.UserId).HasColumnType("CHAR(36)");
            b.OwnsOne(x => x.Data);
        });

    }

    private static void ConfigurePropertiesForInterface<TInterface>(ModelBuilder builder, Action<EntityTypeBuilder, Type> configure)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(TInterface).IsAssignableFrom(entityType.ClrType))
            {
                builder.Entity(entityType.ClrType, b => configure(b, entityType.ClrType));
            }
        }
    }
}
