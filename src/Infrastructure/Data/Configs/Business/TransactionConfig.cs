
using Domain.Orders.Payments;
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
               
        builder.ToTable("Payments");
    }
}
