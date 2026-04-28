
using Domain.Common.Rules;
using Domain.Orders.OrderItems;
using Domain.ReturnItemRequests;
using Domain.ReturnItemRequests.ValueObjects;

namespace Infrastructure.Data.Configs.Business;

internal sealed class ReturnItemRequestConfig : BaseEntityConfig<ReturnItemRequest>
{
    public override void Configure(EntityTypeBuilder<ReturnItemRequest> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => new ReturnItemRequestId(value))
               .ValueGeneratedNever();

        builder.Property(x => x.CustomerMessage)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(ReturnItemRequestRules.MaxCustomerMessageLength)
               .IsRequired(false);

        builder.Property(x => x.Type)
               .HasConversion<string>()
               .HasColumnType("VARCHAR(50)")
               .IsRequired();

        builder.Property(x => x.ReasonType)
               .HasColumnType("VARCHAR(50)")
               .HasConversion<string>()
               .IsRequired();

        builder.Property(x => x.ShippingFees)
               .IsRequired();

        builder.HasOne<OrderItem>()
               .WithMany()
               .HasForeignKey(x => x.OrderItemId)
               .IsRequired();

        builder.OwnsMany(x => x.Attachments, l =>
        {
            l.ToJson();
        });
               

        builder.ToTable("ReturnItemRequests");
    }
}
