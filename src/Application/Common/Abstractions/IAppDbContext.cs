
namespace Application.Common.Abstractions;

public interface IAppDbContext
{
    // add your Entities Set
    DbSet<Customer> Customers {get; }
    DbSet<Order> Orders {get; }
    DbSet<OrderItem> OrderItems {get; }
    DbSet<Review> Reviews {get; }
    DbSet<ProductImage> ProductImages {get; }
    DbSet<Product> Products {get; }
    DbSet<CustomerAddress> CustomerAddresses {get; }
    DbSet<CartItem> CartItems {get; }
    DbSet<Payment> Payments {get; }
    DbSet<Shipment> Shipments {get; }

    Task<int> SaveChangesAsync(CancellationToken token);
}
