namespace Infrastructure.Data.Configs.Business;

public sealed class OrderConfig : BaseEntityConfig<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever()
               .HasConversion(
                   id => id.Value,
                   value => new OrderId(value)
               );

        builder.Property(x => x.TotalAmount)
               .HasColumnType("DECIMAL(9,2)")
               .IsRequired();

        builder.HasOne(x => x.CustomerInfo)
               .WithMany()
               .HasForeignKey(x => x.CustomerId)
               .IsRequired();
               
        builder.ToTable("Orders");
    }
}
