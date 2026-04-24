
using Domain.Common.Entities.Addresses;
using Domain.Warehouses;

namespace Infrastructure.Data.Configs.Business;

internal sealed class WarehouseConfig : BaseEntityConfig<Warehouse>
{
    public override void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => new WarehouseId(value))
               .ValueGeneratedNever();

        builder.Property(x => x.Name)
               .HasColumnType("NVARCHAR(50)")
               .IsRequired();

        builder.Property(x => x.CountryCode)
               .HasColumnType("CHAR(2)")
               .IsRequired();

        builder.HasOne<Address>()
               .WithOne()
               .HasForeignKey<Warehouse>(x => x.AddressId)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();

        builder.ToTable("Warehouses");
    }
}
