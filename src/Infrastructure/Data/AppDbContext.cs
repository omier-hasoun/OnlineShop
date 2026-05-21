
using Domain.Orders;
using Domain.ProductsGroups;
using Domain.Orders.OrderPayments;
using Domain.Orders.OrderItems;
using Domain.ProductsGroups.Products;
using Domain.Brands;
using Domain.Warehouses;
using Domain.PaymentProviders;
using Domain.Categories;
using Domain.ReturnItemRequests;
using Domain.Common.Entities.Addresses;
using Domain.Orders.Shipments;
using Domain.ProductReviews;
using Domain.Transactions;
using Domain.ReturnItemRequestsReviews;
using Domain.UsersPaymentMethodsLogs;
using Infrastructure.Common.EfCore.ValueConverters;
using Infrastructure.Common.EfCore.ValueComparers;
using Domain.Inventories;
using Domain.Common.ValueObjects;
using Domain.ShippingAddresses;
using Application.Entities;
using Domain.Carts.CartItems;
using Domain.Carts;
using Domain.Countries;
using Domain.Currencies;
using Domain.Countries.StateProvinces;


namespace Infrastructure.Data;

public sealed class AppDbContext : IdentityDbContext<AppUser, Role, Guid, UserClaim, IdentityUserRole<Guid>, UserLoginProvider, RoleClaim, UserToken, IdentityUserPasskey<Guid>>, IAppDbContext
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<ProductReview> Reviews => Set<ProductReview>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Shipment> Shipments => Set<Shipment>();
    public DbSet<ProductReview> ProductReviews => Set<ProductReview>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductsGroup> ProductGroups => Set<ProductsGroup>();

    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<AppSettings> AppSettings => Set<AppSettings>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();
    public DbSet<OrderPayment> OrderPayments => Set<OrderPayment>();
    public DbSet<PaymentProvider> PaymentProviders => Set<PaymentProvider>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Inventory> ProductStocks => Set<Inventory>();
    public DbSet<ReturnItemRequest> ReturnRequests => Set<ReturnItemRequest>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<ShippingAddress> ShippingAddresses => Set<ShippingAddress>();
    public DbSet<ReturnItemRequestReview> ReturnItemRequestReviews => Set<ReturnItemRequestReview>();
    public DbSet<UserPaymentMethodLog> UserPaymentMethodLogs => Set<UserPaymentMethodLog>();

    public DbSet<Country> Countries => Set<Country>();

    public DbSet<Currency> Currencies => Set<Currency>();

    public DbSet<StateProvince> StateProvinces => Set<StateProvince>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public async Task SaveAsync(CancellationToken ct = default)
    {
        await SaveChangesAsync(ct);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        ConfigureISoftDeleted(builder);

        ConfigureIHasCreationTime(builder);
        ConfigureIHasModificationTime(builder);
        ConfigureIModificationAudited(builder);
        ConfigureICreationAudited(builder);

        ApplySoftDeleteQueryFilterOnAllMembersOfISoftDelete(builder);

        builder.Ignore<IdentityUserPasskey<Guid>>();
        builder.Ignore<RoleClaim>();
        
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<Money>()
               .HaveConversion<MoneyConverter>();
        
        builder.IgnoreAny<DomainEvent>();
    }

    private static void ApplySoftDeleteQueryFilterOnAllMembersOfISoftDelete(ModelBuilder builder)
    {
        var types = builder.Model.GetEntityTypes().ToList();
        foreach (var entityType in types)
        {
            
            if(typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(AppDbContext).GetMethod(nameof(ApplySoftDeleteQueryFilter), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!
                                                 .MakeGenericMethod(entityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }
        }

    }
    private static void ApplySoftDeleteQueryFilter<TEntity>(ModelBuilder builder)
        where TEntity : class, IEntity, ISoftDelete
    {
        builder.Entity<TEntity>().HasQueryFilter(e => e.IsDeleted == false);
    }

    private static void ConfigureIHasCreationTime(ModelBuilder builder)
    {
        ConfigurePropertiesForInterface<IHasCreationTime>(builder, (b, type) =>
        {
            b.Property(nameof(IHasCreationTime.CreatedAt))
             .IsRequired();
        });
    }
    private static void ConfigureIHasModificationTime(ModelBuilder builder)
    {
        ConfigurePropertiesForInterface<IHasModificationTime>(builder, (b, type) =>
        {
            b.Property(nameof(IHasModificationTime.LastModifiedAt))
             .IsRequired();

        });
    }
    private static void ConfigureICreationAudited(ModelBuilder builder)
    {
        ConfigurePropertiesForInterface<ICreationAudited>(builder, (b, type) =>
        {
            b.Property(nameof(ICreationAudited.CreatedBy))
             .IsRequired();

            b.HasOne(typeof(AppUser))
             .WithMany()
             .HasForeignKey(nameof(ICreationAudited.CreatedBy))
             .OnDelete(DeleteBehavior.Restrict);
        });
    }
    private static void ConfigureIModificationAudited(ModelBuilder builder)
    {
        ConfigurePropertiesForInterface<IModificationAudited>(builder, (b, type) =>
        {
            b.Property(nameof(IModificationAudited.LastModifiedBy))
             .IsRequired();
            
            b.HasOne(typeof(AppUser))
             .WithMany()
             .HasForeignKey(nameof(IModificationAudited.LastModifiedBy))
             .OnDelete(DeleteBehavior.Restrict);


        });
    }
    private static void ConfigureISoftDeleted(ModelBuilder builder)
    {
        ConfigurePropertiesForInterface<ISoftDelete>(builder, (b, type) =>
        {
            b.Property(nameof(ISoftDelete.IsDeleted))
             .IsRequired();
        });
    }
    internal static void ConfigurePropertiesForInterface<TInterface>(ModelBuilder builder, Action<EntityTypeBuilder, Type> configure)
    {
        var entityTypes = builder.Model.GetEntityTypes().ToList();

        foreach (var entityType in entityTypes)
        {
            if (typeof(TInterface).IsAssignableFrom(entityType.ClrType))
            {
                builder.Entity(entityType.ClrType, b => configure(b, entityType.ClrType));
            }
        }
    }
}
