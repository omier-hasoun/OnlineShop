


using Domain.Addresses;
using Domain.CartItems;
using Domain.Orders.OrderItems;
using Domain.Products.ProductImages;
using Domain.Products.ProductReviews;
using Domain.Shipments;

namespace Application.Common.Abstractions;

public interface IAppDbContext
{
    // add your Entities Set
    DbSet<Order> Orders {get; }
    DbSet<OrderItem> OrderItems {get; }
    DbSet<ProductReview> Reviews {get; }
    DbSet<ProductImage> ProductImages {get; }
    DbSet<Product> Products {get; }
    DbSet<Address> CustomerAddresses {get; }
    DbSet<CartItem> CartItems {get; }
    DbSet<Shipment> Shipments {get; }

    Task<int> SaveChangesAsync(CancellationToken token);
}
