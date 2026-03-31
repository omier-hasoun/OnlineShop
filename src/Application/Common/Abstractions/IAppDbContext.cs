


using Domain.Addresses;
using Domain.CartItems;
using Domain.Carts;
using Domain.Orders.Items;
using Domain.Products.Images;
using Domain.Shipments;

namespace Application.Common.Abstractions;

public interface IAppDbContext
{
    // add your Entities Set
    DbSet<User> Customers {get; }
    DbSet<Order> Orders {get; }
    DbSet<OrderItem> OrderItems {get; }
    DbSet<ProductReview> Reviews {get; }
    DbSet<ProductImage> ProductImages {get; }
    DbSet<Product> Products {get; }
    DbSet<Address> CustomerAddresses {get; }
    DbSet<CartItem> CartItems {get; }
    DbSet<Transaction> Payments {get; }
    DbSet<Shipment> Shipments {get; }
    DbSet<Cart> Carts {get; }

    Task<int> SaveChangesAsync(CancellationToken token);
}
