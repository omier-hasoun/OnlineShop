using Domain.Brands;
using Domain.Categories;
using Domain.Common.Entities.Addresses;
using Domain.Orders.OrderItems;
using Domain.Orders.Shipments;
using Domain.PaymentProviders;
using Domain.ProductReviews;
using Domain.ProductsGroups.Products;
using Domain.ProductsStock;
using Domain.ReturnItemRequestsReviews;
using Domain.ReturnItemRequests;
using Domain.Transactions;
using Domain.UsersPaymentMethodsLogs;
using Domain.Warehouses;
using Domain.ShippingAddresses;
using Application.Entities;
using Domain.Carts.CartItems;
using Domain.Carts;

namespace Application.Common.Abstractions;

public interface IAppDbContext

{
    DbSet<AppUser> Users {get; }

    DbSet<Order> Orders {get; }
    DbSet<OrderItem> OrderItems {get; }

    DbSet<ProductReview> ProductReviews {get; }
    DbSet<ProductsGroup> ProductGroups {get; }
    DbSet<Product> Products { get; }

    DbSet<Brand> Brands { get; }

    DbSet<AppSettings> AppSettings { get; }

    DbSet<Warehouse> Warehouses { get; }
    DbSet<ProductStock> ProductStocks { get; }

    DbSet<OrderPayment> OrderPayments { get; }

    DbSet<PaymentProvider> PaymentProviders { get; }
    DbSet<Transaction> Transactions { get; }
    DbSet<UserPaymentMethodLog> UserPaymentMethodLogs { get; }

    DbSet<Category> Categories { get; }

    DbSet<ReturnItemRequest> ReturnRequests { get; }
    DbSet<ReturnItemRequestReview> ReturnItemRequestReviews { get; }

    DbSet<Address> Addresses {get; }
    DbSet<ShippingAddress> ShippingAddresses { get; }

    DbSet<Cart> Carts { get; }

    DbSet<CartItem> CartItems {get; }

    DbSet<Shipment> Shipments {get; }

    Task SaveAsync(CancellationToken ct = default);
}
