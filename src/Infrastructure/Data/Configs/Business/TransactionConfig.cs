
using Domain.Transactions;
using Infrastructure.Common.EfCore.ValueComparers;
using Infrastructure.Common.EfCore.ValueConverters;

namespace Infrastructure.Data.Configs.Business;

internal sealed class TransactionConfig : BaseEntityConfig<Transaction>
{
    public override void Configure(EntityTypeBuilder<Transaction> builder)
    {
        base.Configure(builder);

        builder.Ignore(x => x.AdditionalDetails);

        builder.HasKey(e => e.Id)
               .IsClustered();
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


        builder.Property(x => x.CardFingerprint)
               .HasColumnType("NVARCHAR(255)")
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

        builder.Property("_additionalDetails")
                  .HasColumnName("AdditionalDetails")
                  .HasConversion<JsonConverter<Dictionary<string, string>>>(new JsonDictionaryValueComparer())
                  .IsRequired(false);

        builder.ToTable("Transactions");
    }
}
