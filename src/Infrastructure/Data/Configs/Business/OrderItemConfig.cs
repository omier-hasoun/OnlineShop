namespace Infrastructure.Data.Configs.Business;

public sealed class OrderItemConfig : BaseEntityConfig<OrderItem>
{
    public override void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => new { x.OrderId, x.ProductId});

        builder.Property(x => x.UnitPrice)
               .HasColumnType("DECIMAL(9,2)")
               .IsRequired();
               
        builder.Property(x => x.TotalPrice)
               .HasColumnType("DECIMAL(9,2)")
               .IsRequired();

        builder.ToTable("OrderItems");
    }
}
