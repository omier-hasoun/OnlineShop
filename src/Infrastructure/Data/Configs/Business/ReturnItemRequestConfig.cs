
using Domain.Common.Rules;
using Domain.Orders.OrderLines;
using Domain.ReturnItemRequests;
using Domain.ReturnItemRequests.ValueObjects;

namespace Infrastructure.Data.Configs.Business;

internal sealed class ReturnItemRequestConfig : BaseEntityConfig<ReturnItemRequest>
{
    public override void Configure(EntityTypeBuilder<ReturnItemRequest> builder)
    {
        base.Configure(builder);


        builder.HasKey(e => e.Id)
               .IsClustered();

        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => new ReturnItemRequestId(value))
               .ValueGeneratedNever();

        builder.Property(x => x.CustomerMessage)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(ReturnItemRequestRules.MaxCustomerMessageLength)
               .IsRequired(false);

        builder.Property(x => x.Type)
               .HasConversion<int>()
               .IsRequired();

        builder.Property(x => x.ReasonType)
               .HasConversion<int>()
               .IsRequired();

        builder.Property(x => x.Status)
               .HasConversion<int>()
               .IsRequired();

        builder.Property(x => x.ShippingFees)
               .IsRequired();

        builder.HasOne<OrderLine>()
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
