

using Domain.Orders.Items;

namespace Infrastructure.Data.Configs.Business;

public sealed class OrderItemConfig : BaseEntityConfig<OrderItem>
{
    public override void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => new OrderItemId(value))
               .ValueGeneratedNever();

        builder.Property(x => x.UnitPrice)
               .HasColumnType("DECIMAL(9,2)")
               .IsRequired();

        builder.Property(x => x.TotalPrice)
               .HasColumnType("DECIMAL(9,2)")
               .IsRequired();

        builder.HasOne(x => x.OrderInfo)
               .WithMany(x => x.OrderItems)
               .HasForeignKey(x => x.OrderId);

        builder.HasOne(x => x.ProductInfo)
               .WithMany()
               .HasForeignKey(x => x.ProductId);

        builder.ToTable("OrderItems");
    }
}
