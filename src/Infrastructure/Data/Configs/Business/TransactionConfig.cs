
using Domain.Orders.Payments;
using Domain.Transactions;

namespace Infrastructure.Data.Configs.Business;

public sealed class PaymentConfig : BaseEntityConfig<Transaction>
{
    public override void Configure(EntityTypeBuilder<Transaction> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => new TransactionId(value))
               .ValueGeneratedNever();

        builder.Property(x => x.PaidAmount)
               .HasColumnType("DECIMAL(9,2)")
               .IsRequired();

        builder.HasIndex(x => x.TransactionId)
               .HasDatabaseName("IX_Payment_TransactionId");

        builder.HasOne(x => x.OrderInfo)
               .WithOne()
               .HasForeignKey<Transaction>(x => x.OrderId);
               
        builder.ToTable("Payments");
    }
}
