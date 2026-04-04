
using Domain.Orders;
using Domain.Products;
using Domain.Addresses;
using Domain.Shipments;
using Domain.CartItems;
using Domain.Orders.Payments;
using Infrastructure.Data.LinkEntities;
using Domain.Orders.OrderItems;
using Domain.Products.ProductImages;
using Domain.Products.ProductReviews;

namespace Infrastructure.Data;

public sealed class AppDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRoles, UserLoginProvider, RoleClaim, UserToken, IdentityUserPasskey<Guid>>, IAppDbContext
{
    public DbSet<User> Customers => Set<User>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<ProductReview> Reviews => Set<ProductReview>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Address> CustomerAddresses => Set<Address>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Shipment> Shipments => Set<Shipment>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
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

        ConfigureIDeletionMetadata(builder);
        ConfigureISoftDeletable(builder);

        ConfigureIHasCreationTime(builder);
        ConfigureIHasModificationTime(builder);

        ConfigureIModificationAudited(builder);
        ConfigureICreationAudited(builder);

        builder.Entity<IdentityUserPasskey<Guid>>(b =>
        {
            b.HasKey(p => new { p.UserId, p.CredentialId });
            b.Property(p => p.UserId).HasColumnType("CHAR(36)");
            b.OwnsOne(x => x.Data);
        });

    }

    private void ConfigureIDeletionMetadata(ModelBuilder builder)
    {
        ConfigurePropertiesForInterface<IDeletionMetadata>(builder, (b, type) =>
        {
            b.Property(nameof(IDeletionMetadata.DeletedAt))
             .IsRequired();

            b.Property(nameof(IDeletionMetadata.DeletedBy))
             .HasConversion<Guid>()
             .IsRequired();
        });

        //}//        builder.Property(x => x.CreatedBy)
        //       .HasColumnType("CHAR(36)")
        //       .IsRequired();

        //builder.Property(x => x.LastModifiedBy)
        //       .HasColumnType("CHAR(36)")
        //       .IsRequired();

        //builder.Property(x => x.LastModifiedAt)
        //        .IsRequired();

        //builder.Property(x => x.CreatedAt)
        //       .IsRequired();
    }
    private void ConfigureIHasCreationTime(ModelBuilder builder)
    {
        ConfigurePropertiesForInterface<IHasCreationTime>(builder, (b, type) =>
        {
            b.Property(nameof(IHasCreationTime.CreatedAt))
             .IsRequired();
        });
    }
    private void ConfigureIHasModificationTime(ModelBuilder builder)
    {
        ConfigurePropertiesForInterface<IHasModificationTime>(builder, (b, type) =>
        {
            b.Property(nameof(IHasModificationTime.LastModifiedAt))
             .IsRequired();
        });
    }
    private void ConfigureICreationAudited(ModelBuilder builder)
    {
        ConfigurePropertiesForInterface<ICreationAudited>(builder, (b, type) =>
        {
            b.Property(nameof(ICreationAudited.CreatedBy))
             .IsRequired();
        });
    }
    private void ConfigureIModificationAudited(ModelBuilder builder)
    {
        ConfigurePropertiesForInterface<IModificationAudited>(builder, (b, type) =>
        {
            b.Property(nameof(IModificationAudited.LastModifiedBy))
             .IsRequired();
        });
    }
    private void ConfigureISoftDeletable(ModelBuilder builder)
    {
        ConfigurePropertiesForInterface<ISoftDeletable>(builder, (b, type) =>
        {
            b.Property(nameof(ISoftDeletable.IsDeleted))
             .IsRequired();
        });
    }
    private void ConfigurePropertiesForInterface<TInterface>(ModelBuilder builder, Action<EntityTypeBuilder, Type> configure)
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
