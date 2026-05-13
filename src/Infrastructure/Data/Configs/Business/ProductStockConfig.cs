
using Domain.ProductsGroups.Products;
using Domain.ProductsStock;
using Domain.Warehouses;

namespace Infrastructure.Data.Configs.Business;

internal sealed class ProductStockConfig : BaseEntityConfig<ProductStock>
{
    public override void Configure(EntityTypeBuilder<ProductStock> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => new { x.ProductId, x.WarehouseId })
               .IsClustered();

        builder.HasOne<Product>()
               .WithMany()
               .HasForeignKey(x => x.ProductId)
               .IsRequired();

        builder.HasOne<Warehouse>()
               .WithMany()
               .HasForeignKey(x => x.WarehouseId)
               .IsRequired();

        builder.HasIndex(x => x.WarehouseId)
               .HasDatabaseName("IX_ProductsStock_WarehouseId");

        builder.ToTable("ProductsStock", x =>
            {
                x.HasCheckConstraint("CK_ProductsStock_ReservedQuantity", "[ReservedQuantity] <= [Quantity]");
            }
        );

    }
}
