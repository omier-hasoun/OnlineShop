using Domain.Orders;
using Domain.Shipments;

namespace Infrastructure.Data.Configs.Business;

public sealed class ShipmentConfig : BaseEntityConfig<Shipment>
{
    public override void Configure(EntityTypeBuilder<Shipment> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever()
               .HasConversion(
                   id => id.Value,
                   value => new ShipmentId(value)
               );

        builder.Property(x => x.CarrierName)
               .HasColumnType("VARCHAR(32)")
               .IsRequired();

        builder.Property(x => x.TrackingNumber)
               .HasColumnType("VARCHAR(36)")
               .IsRequired();

        builder.Property(x => x.Notes)
               .HasColumnType("VARCHAR(64)")
               .IsRequired(false);

        builder.HasOne<Order>()
               .WithOne(x => x.ShipmentInfo)
               .HasForeignKey<Shipment>(x => x.OrderId)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();

        builder.HasOne(x => x.AddressInfo)
               .WithOne()
               .HasForeignKey<Shipment>(x => x.AddressId)
                .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();

        builder.HasIndex(x => x.TrackingNumber)
               .HasDatabaseName("IX_Shipment_TrackingNumber");

        builder.ToTable("Shipments");

    }
}
