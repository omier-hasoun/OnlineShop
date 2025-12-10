namespace Infrastructure.Data.Configs.Business;

public sealed class PaymentConfig : BaseEntityConfig<Payment>
{
    public override void Configure(EntityTypeBuilder<Payment> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.OrderId);

        builder.Property(x => x.PaidAmount)
               .HasColumnType("DECIMAL(9,2)")
               .IsRequired();

        builder.HasIndex(x => x.TransactionId)
               .HasDatabaseName("IX_Payment_TransactionId");

        builder.HasOne(x => x.OrderInfo)
               .WithOne()
               .HasForeignKey<Payment>(x => x.OrderId);
               
        builder.ToTable("Payments");
    }
}
