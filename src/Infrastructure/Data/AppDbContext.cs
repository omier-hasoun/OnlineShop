
using Domain.Orders;
using Domain.Products;
using Domain.Orders.OrderPayments;
using Domain.Orders.OrderItems;
using Domain.Products.ProductImages;
using Infrastructure.Data.Interceptors;
using Domain.Products.ProductVariants;
using Domain.Brands;
using Domain.AppSettings;
using Domain.Warehouses;
using Domain.PaymentProviders;
using Domain.Categories;
using Domain.ProductStocks;
using Domain.ReturnItemRequests;
using Domain.Common.Entities.Addresses;
using Domain.Customers.CartItems;
using Domain.Orders.Shipments;
using Domain.ProductReviews;
using Domain.ReturnItemRequests.Attachments;
using Domain.Transactions;
using Domain.Customers;
using Domain.ReturnItemRequestReviews;
using Domain.UserPaymentMethodLogs;
using Application.Common.Identity;


namespace Infrastructure.Data;

public sealed class AppDbContext : IdentityDbContext<AppUser, Role, Guid, UserClaim, IdentityUserRole<Guid>, UserLoginProvider, RoleClaim, UserToken, IdentityUserPasskey<Guid>>, IAppDbContext
{
    public DbSet<AppUser> Customers => Set<AppUser>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<ProductReview> Reviews => Set<ProductReview>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
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
    public DbSet<ReturnItemRequestAttachment> ReturnRequestAttachments => Set<ReturnItemRequestAttachment>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<CustomerShippingAddress> CustomerShippingAddresses => Set<CustomerShippingAddress>();
    public DbSet<ReturnItemRequestReview> ReturnItemRequestReviews => Set<ReturnItemRequestReview>();
    public DbSet<UserPaymentMethodLog> UserPaymentMethodLogs => Set<UserPaymentMethodLog>();


    #region notSupported
    private string _notSupportedMessage = "The invoked DbSet not supported, just ignore it dont remove them";
    public override DbSet<RoleClaim> RoleClaims { get => throw new NotSupportedException(_notSupportedMessage); set { throw new NotSupportedException(_notSupportedMessage); } }
    public override DbSet<IdentityUserPasskey<Guid>> UserPasskeys { get => throw new NotSupportedException(_notSupportedMessage); set => throw new NotSupportedException(_notSupportedMessage); }
    #endregion



    private readonly IServiceProvider _serviceProvider;

    public AppDbContext(DbContextOptions<AppDbContext> options, IServiceProvider sp) : base(options)
    {
        _serviceProvider = sp;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {

        return await base.SaveChangesAsync(ct);
    }




    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // order is important here so be aware when you modify this.
        optionsBuilder.AddInterceptors(
            _serviceProvider.GetRequiredService<SoftDeleteEntitySaveChangesInterceptor>(),
            _serviceProvider.GetRequiredService<AuditedEntitySaveChangesInterceptor>()
            );

       
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        ConfigureIDeletionMetadata(builder);
        ConfigureISoftDeleted(builder);

        ConfigureIHasCreationTime(builder);
        ConfigureIHasModificationTime(builder);

        ConfigureIModificationAudited(builder);
        ConfigureICreationAudited(builder);

        builder.Ignore<IdentityUserPasskey<Guid>>();
        builder.Ignore<RoleClaim>();

        ApplySoftDeleteQueryFilterOnAllChilds(builder);
    }










    private static void ApplySoftDeleteQueryFilterOnAllChilds(ModelBuilder builder)
    {
        foreach(var entityType in builder.Model.GetEntityTypes())
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
        where TEntity : class, ISoftDeleted
    {
        builder.Entity<TEntity>().HasQueryFilter(e => e.IsDeleted == false);
    }
    private static void ConfigureIDeletionMetadata(ModelBuilder builder)
    {
        ConfigurePropertiesForInterface<IDeletionMetadata>(builder, (b, type) =>
        {
            b.Property(nameof(IDeletionMetadata.DeletedAt))
             .IsRequired();

            b.Property(nameof(IDeletionMetadata.DeletedBy))
             .HasConversion<Guid>()
             .IsRequired();
        });
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
        });
    }
    private static void ConfigureIModificationAudited(ModelBuilder builder)
    {
        ConfigurePropertiesForInterface<IModificationAudited>(builder, (b, type) =>
        {
            b.Property(nameof(IModificationAudited.LastModifiedBy))
             .IsRequired();
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
