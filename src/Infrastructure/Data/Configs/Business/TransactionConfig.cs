
using System.Text.Json;
using Domain.Transactions;

namespace Infrastructure.Data.Configs.Business;

public sealed class TransactionConfig : BaseEntityConfig<Transaction>
{
    public override void Configure(EntityTypeBuilder<Transaction> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => new TransactionId(value))
               .ValueGeneratedNever();

        builder.Property(x => x.SenderType)
               .HasColumnType("VARCHAR(50)")
               .HasConversion<string>()
               .IsRequired();

        builder.Property(x => x.ReceiverType)
               .HasColumnType("VARCHAR(50)")
               .HasConversion<string>()
               .IsRequired();

        builder.Property(x => x.PaymentProviderName)
               .HasColumnType("VARCHAR(50)")
               .IsRequired();

        builder.Property(x => x.ExternalTransactionId)
               .HasColumnType("VARCHAR(50)")
               .IsRequired(false);

        builder.Property(x => x.TransferAmount)
               .IsRequired();

        builder.Property(x => x.Notes)
               .HasColumnType("NVARCHAR(1000)")
               .IsRequired(false);

        builder.Property(x => x.Status)
               .HasColumnType("VARCHAR(50)")
               .HasConversion<string>()
               .IsRequired();

        builder.Property(x => x.ReceiverId)
               .HasColumnType("VARCHAR(100)")
               .IsRequired();

        builder.Property(x => x.SenderId)
               .HasColumnType("VARCHAR(100)")
               .IsRequired();

        builder.ToTable("Transactions");
    }
}
