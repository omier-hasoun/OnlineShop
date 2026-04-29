using Domain.Common.Rules;
using Domain.Orders;
using Domain.Orders.Shipments;

namespace Infrastructure.Data.Configs.Business;

internal sealed class ShipmentConfig : BaseEntityConfig<Shipment>
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
               .HasColumnType("NVARCHAR(50)")
               .IsRequired();

        builder.Property(x => x.AddressFrom)
               .HasColumnType("NVARCHAR(500)")
               .IsRequired();

        builder.Property(x => x.AddressTo)
               .HasColumnType("NVARCHAR(500)")
               .IsRequired();

        builder.Property(x => x.TrackingNumber)
               .HasColumnType("VARCHAR")
               .HasMaxLength(100)
               .IsRequired(false);

        builder.Property(x => x.Notes)
               .HasColumnType("VARCHAR")
               .HasMaxLength(ShipmentRules.MaxNotesLength)
               .IsRequired(false);

        builder.Property(x => x.Status)
               .HasColumnType("VARCHAR(50)")
               .HasConversion<string>()
               .IsRequired();

        builder.HasOne<Order>()
               .WithMany(x => x.Shipments)
               .HasForeignKey(x => x.OrderId)
               .IsRequired();

        builder.ToTable("Shipments");

    }
}
