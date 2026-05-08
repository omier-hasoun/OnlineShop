

using Domain.Orders;
using Domain.Orders.OrderItems;
using Domain.Products.ProductVariants;
using Infrastructure.Common.EfCore.ValueComparers;
using Infrastructure.Common.EfCore.ValueConverters;

namespace Infrastructure.Data.Configs.Business;

internal sealed class OrderItemConfig : BaseEntityConfig<OrderItem>
{
    public override void Configure(EntityTypeBuilder<OrderItem> builder)
    {

        builder.HasKey(e => e.Id)
               .IsClustered();

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
               .HasColumnType("NVARCHAR(MAX)")
               .HasConversion<JsonConverter<List<string>>>(new JsonListValueComparer())
               .IsRequired(false);

        builder.HasOne<Order>()
               .WithMany(x => x.Items)
               .HasForeignKey(x => x.OrderId)
               .IsRequired();

        builder.OwnsOne(x => x.ProductInfo, b =>
        {
            b.ToJson();
            b.OwnsOne(x => x.Attributes);
            b.OwnsOne(x => x.VariantSpecification);


        });

        builder.HasOne<ProductVariant>()
               .WithMany()
               .HasForeignKey(x => x.ProductVariantId)
               .IsRequired();

        builder.ToTable("OrderItems");
    }
}
