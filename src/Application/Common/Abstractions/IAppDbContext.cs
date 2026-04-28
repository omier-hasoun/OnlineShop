using Application.Common.Identity;
using Domain.AppSettings;
using Domain.Brands;
using Domain.Categories;
using Domain.Common.Entities.Addresses;
using Domain.Customers;
using Domain.Customers.CartItems;
using Domain.Orders.OrderItems;
using Domain.Orders.Shipments;
using Domain.PaymentProviders;
using Domain.ProductReviews;
using Domain.Products.ProductVariants;
using Domain.ProductsStock;
using Domain.ReturnItemRequestsReviews;
using Domain.ReturnItemRequests;
using Domain.Transactions;
using Domain.UsersPaymentMethodsLogs;
using Domain.Warehouses;
using Domain.Customers.CustomerShippingAddresses;

namespace Application.Common.Abstractions;

public interface IAppDbContext

{
    public DbSet<AppUser> Users {get; }

    DbSet<Order> Orders {get; }
    DbSet<OrderItem> OrderItems {get; }
    DbSet<ProductReview> ProductReviews {get; }
    DbSet<Product> Products {get; }
    DbSet<ProductVariant> ProductVariants { get; }
    DbSet<Brand> Brands { get; }
    DbSet<AppSettings> AppSettings { get; }
    DbSet<Warehouse> Warehouses { get; }
    DbSet<OrderPayment> OrderPayments { get; }
    DbSet<PaymentProvider> PaymentProviders { get; }
    DbSet<Category> Categories { get; }

    DbSet<ProductStock> ProductStocks { get; }
    DbSet<ReturnItemRequest> ReturnRequests { get; }
    DbSet<Transaction> Transactions { get; }
    DbSet<Customer> Customers { get; }
    DbSet<Address> Addresses {get; }
    DbSet<CustomerShippingAddress> CustomerShippingAddresses { get; }

    DbSet<ReturnItemRequestReview> ReturnItemRequestReviews { get; }
    DbSet<UserPaymentMethodLog> UserPaymentMethodLogs { get; }
    DbSet<CartItem> CartItems {get; }
    DbSet<Shipment> Shipments {get; }

    Task<bool> SaveAsync(CancellationToken ct = default);
}
