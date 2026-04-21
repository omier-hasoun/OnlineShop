using Domain.AppSettings;
using Domain.Brands;
using Domain.Categories;
using Domain.Common.Entities.Addresses;
using Domain.Orders.OrderItems;
using Domain.Orders.Shipments;
using Domain.PaymentProviders;
using Domain.Products.ProductImages;
using Domain.Products.ProductVariants;
using Domain.ProductStocks;
using Domain.ReturnItemRequests;
using Domain.Customers.CartItems;
using Domain.Warehouses;
using Domain.ProductReviews;
using Domain.Customers;
using Domain.UserPaymentMethodLogs;
using Domain.ReturnItemRequestReviews;
using Domain.ReturnItemRequests.Attachments;
using Domain.Transactions;

namespace Application.Common.Abstractions;

public interface IAppDbContext
{
    DbSet<Order> Orders {get; }
    DbSet<OrderItem> OrderItems {get; }
    DbSet<ProductReview> ProductReviews {get; }
    DbSet<ProductImage> ProductImages {get; }
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
    DbSet<ReturnItemRequestAttachment> ReturnRequestAttachments { get; }

    DbSet<Transaction> Transactions { get; }

    DbSet<Address> Addresses {get; }
    DbSet<CustomerShippingAddress> CustomerShippingAddresses { get; }

    DbSet<ReturnItemRequestReview> ReturnItemRequestReviews { get; }
    DbSet<UserPaymentMethodLog> UserPaymentMethodLogs { get; }
    DbSet<CartItem> CartItems {get; }
    DbSet<Shipment> Shipments {get; }

    Task<int> SaveChangesAsync(CancellationToken token = default);
}
