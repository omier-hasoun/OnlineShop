
using Domain.ProductsGroups.Products;
using Domain.Inventories;
using Domain.Warehouses;

namespace Infrastructure.Data.Configs.Business;

internal sealed class InventoryConfig : BaseEntityConfig<Inventory>
{
    public override void Configure(EntityTypeBuilder<Inventory> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => new { x.ProductId, x.WarehouseId })
               .IsClustered();

        builder.HasOne<Product>()
               .WithMany()
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.HasOne<Warehouse>()
               .WithMany()
               .HasForeignKey(x => x.WarehouseId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.HasIndex(x => x.WarehouseId)
               .HasDatabaseName("IX_Inventories_WarehouseId");

        builder.ToTable("Inventories", x =>
            {
                x.HasCheckConstraint("CK_Inventories_ReservedQuantity", "[ReservedQuantity] <= [Quantity]");
            }
        );

    }
}
