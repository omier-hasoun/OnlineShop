

using Domain.Orders;
using Domain.Orders.OrderItems;
using Domain.ProductGroups.Products;
using Infrastructure.Common.EfCore.ValueComparers;
using Infrastructure.Common.EfCore.ValueConverters;

namespace Infrastructure.Data.Configs.Business;

internal sealed class OrderLineConfig : BaseEntityConfig<OrderLine>
{
    public override void Configure(EntityTypeBuilder<OrderLine> builder)
    {

        builder.HasKey(e => e.Id)
               .IsClustered();

        builder.Ignore(x => x.SerialNumbers);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => new OrderLineId(value))
               .ValueGeneratedNever();

        builder.Property(x => x.UnitPrice)
               .IsRequired();

        builder.Property(x => x.TotalPrice)
               .IsRequired();


        builder.Property(x => x.TaxAmount)
               .IsRequired();

        builder.Property(x => x.Status)
               .HasConversion<int>()
               .IsRequired();


        builder.Property(x => x.ProductTitleSnapshot)
               .HasColumnType("NVARCHAR(255)")
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

        builder.HasOne<Product>()
               .WithMany()
               .HasForeignKey(x => x.ProductId)
               .IsRequired();

        builder.ToTable("OrderItems");
    }
}
