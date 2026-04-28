
using Domain.Orders;
using Domain.Products;
using Domain.Orders.OrderPayments;
using Domain.Orders.OrderItems;
using Domain.Products.ProductVariants;
using Domain.Brands;
using Domain.Warehouses;
using Domain.PaymentProviders;
using Domain.Categories;
using Domain.ReturnItemRequests;
using Domain.Common.Entities.Addresses;
using Domain.Customers.CartItems;
using Domain.Orders.Shipments;
using Domain.ProductReviews;
using Domain.Transactions;
using Domain.Customers;
using Domain.ReturnItemRequestsReviews;
using Domain.UsersPaymentMethodsLogs;
using Infrastructure.Common.EfCore.ValueConverters;
using Infrastructure.Common.EfCore.ValueComparers;
using Domain.ProductsStock;
using Domain.Common.ValueObjects;
using Domain.Customers.CustomerShippingAddresses;
using Domain.Products.ValueObjects;
using Application.Common.AppSettingsConfiguration;


namespace Infrastructure.Data;

internal sealed class AppDbContext : IdentityDbContext<AppUser, Role, Guid, UserClaim, IdentityUserRole<Guid>, UserLoginProvider, RoleClaim, UserToken, IdentityUserPasskey<Guid>>, IAppDbContext
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<ProductReview> Reviews => Set<ProductReview>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Shipment> Shipments => Set<Shipment>();
    public DbSet<ProductReview> ProductReviews => Set<ProductReview>();
    public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<AppSettings> AppSettings => Set<AppSettings>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();
    public DbSet<OrderPayment> OrderPayments => Set<OrderPayment>();
    public DbSet<PaymentProvider> PaymentProviders => Set<PaymentProvider>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<ProductStock> ProductStocks => Set<ProductStock>();
    public DbSet<ReturnItemRequest> ReturnRequests => Set<ReturnItemRequest>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<CustomerShippingAddress> CustomerShippingAddresses => Set<CustomerShippingAddress>();
    public DbSet<ReturnItemRequestReview> ReturnItemRequestReviews => Set<ReturnItemRequestReview>();
    public DbSet<UserPaymentMethodLog> UserPaymentMethodLogs => Set<UserPaymentMethodLog>();
    public DbSet<Customer> Customers => Set<Customer>();



    //#region notSupported
    //private string _notSupportedMessage = "The invoked DbSet not supported, just ignore it and do not remove them";
    //public override DbSet<RoleClaim> RoleClaims { get => throw new NotSupportedException(_notSupportedMessage); set { throw new NotSupportedException(_notSupportedMessage); } }
    //public override DbSet<IdentityUserPasskey<Guid>> UserPasskeys { get => throw new NotSupportedException(_notSupportedMessage); set => throw new NotSupportedException(_notSupportedMessage); }

    //#endregion

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public async Task<bool> SaveAsync(CancellationToken ct = default)
    {
        return await SaveChangesAsync(ct) > 0;
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
        builder.Properties<CustomerId>()
                .HaveConversion<CustomerIdConverter>();

        builder.Properties<Money>()
               .HaveConversion<MoneyConverter>();
    }

    private static void ApplySoftDeleteQueryFilterOnAllMembersOfISoftDelete(ModelBuilder builder)
    {
        var types = builder.Model.GetEntityTypes().ToList();
        foreach (var entityType in types)
        {
            
            if(typeof(ISoftDeleted).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(AppDbContext).GetMethod(nameof(ApplySoftDeleteQueryFilter), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!
                                                 .MakeGenericMethod(entityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }
        }

    }
    private static void ApplySoftDeleteQueryFilter<TEntity>(ModelBuilder builder)
        where TEntity : class, IEntity, ISoftDeleted
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
        ConfigurePropertiesForInterface<ISoftDeleted>(builder, (b, type) =>
        {
            b.Property(nameof(ISoftDeleted.IsDeleted))
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
