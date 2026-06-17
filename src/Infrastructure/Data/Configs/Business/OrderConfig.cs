
using Application.Entities;
using Domain.Orders;

namespace Infrastructure.Data.Configs.Business;

internal sealed class OrderConfig : BaseEntityConfig<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {

        builder.HasKey(e => e.Id)
               .IsClustered();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever()
               .HasConversion(
                   id => id.Value,
                   value => new OrderId(value)
               );

        builder.Property(x => x.TotalPrice)
               .IsRequired();

        builder.Property(x => x.TotalTaxAmount)
               .IsRequired();

        builder.Property(x => x.ShippingFees)
               .IsRequired();

        builder.Property(x => x.Email)
               .IsRequired();

        builder.HasOne<AppUser>()
               .WithMany()
               .HasForeignKey(x => x.UserId)
               .IsRequired();

        builder.ToTable("Orders");
    }
}
