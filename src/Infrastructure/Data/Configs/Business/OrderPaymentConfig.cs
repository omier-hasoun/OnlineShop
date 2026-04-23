

using Domain.Orders;
using Domain.Orders.OrderPayments;
using Domain.Transactions;
using Domain.UsersPaymentMethodsLogs;

namespace Infrastructure.Data.Configs.Business;

internal sealed class OrderPaymentConfig : BaseEntityConfig<OrderPayment>
{
    public override void Configure(EntityTypeBuilder<OrderPayment> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever();

        builder.Property(x => x.InvoiceFileName)
               .HasColumnType("VARCHAR(50)")
               .IsRequired();

        builder.HasOne<Order>()
               .WithMany(x => x.Payments)
               .HasForeignKey(x => x.OrderId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.HasOne<Transaction>()
               .WithMany()
               .HasForeignKey(x => x.Id)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();

        builder.HasOne<UserPaymentMethodLog>()
               .WithMany()
               .HasForeignKey(x => x.UserPaymentMethodLogId)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();

        builder.ToTable("OrderPayments");
    }
}
