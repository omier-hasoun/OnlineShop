
namespace Infrastructure.Data;

public sealed class AppDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRoles, UserLoginProvider, RoleClaim, UserToken, IdentityUserPasskey<Guid>>, IAppDbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<CustomerAddress> CustomerAddresses => Set<CustomerAddress>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Shipment> Shipments => Set<Shipment>();

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
