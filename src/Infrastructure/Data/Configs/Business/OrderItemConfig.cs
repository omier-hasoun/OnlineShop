

using System.Text.Json;
using Domain.Orders;
using Domain.Orders.OrderItems;
using Domain.Products.ProductVariants;
using Infrastructure.Common.EfCore.ValueComparers;
using Infrastructure.Common.EfCore.ValueConverters;

namespace Infrastructure.Data.Configs.Business;

public sealed class OrderItemConfig : BaseEntityConfig<OrderItem>
{
    public override void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        base.Configure(builder);

        builder.Ignore(x => x.TotalPrice);
        builder.Ignore(x => x.SerialNumbers);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => new OrderItemId(value))
               .ValueGeneratedNever();

        builder.Property(x => x.UnitPrice)
               .IsRequired();

        builder.Property(x => x.Status)
               .HasColumnType("VARCHAR(50)")
               .HasConversion<string>()
               .IsRequired();

        builder.Property("_serialNumbers")
               .HasColumnName("SerialNumbers")
               .HasColumnType("NVARCHAR(3000)")
               .HasConversion<JsonValueConverter<List<string>>>(new JsonListValueComparer())
               .IsRequired(false);

        builder.HasOne<Order>()
               .WithMany(x => x.Items)
               .HasForeignKey(x => x.OrderId)
               .IsRequired();

        builder.HasOne<ProductVariant>()
               .WithMany()
               .HasForeignKey(x => x.ProductVariantId)
               .IsRequired();

        builder.ToTable("OrderItems");
    }
}
