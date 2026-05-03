using Domain.Customers;
using Domain.Orders;

namespace Infrastructure.Data.Configs.Business;

internal sealed class OrderConfig : BaseEntityConfig<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {

        builder.HasKey(e => e.Id)
               .IsClustered();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever()
               .HasConversion(
                   id => id.Value,
                   value => new OrderId(value)
               );

        builder.Property(x => x.TotalItemsPrice)
               .IsRequired();

        builder.Property(x => x.ShippingFees)
               .IsRequired();

        builder.HasOne<Customer>()
               .WithMany()
               .HasForeignKey(x => x.CustomerId)
               .IsRequired();

        builder.ToTable("Orders");
    }
}
