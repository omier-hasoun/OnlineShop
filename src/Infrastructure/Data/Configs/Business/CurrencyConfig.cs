
using Domain.Currencies;

namespace Infrastructure.Data.Configs.Business;

internal sealed class CurrencyConfig : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.HasKey(x => x.Code);

        builder.Property(x => x.Code)
               .HasColumnType("CHAR(3)")
               .ValueGeneratedNever();

        builder.Property(x => x.Name)
               .HasColumnType("NVARCHAR(255)")
               .IsRequired();

        builder.Property(x => x.Symbol)
               .HasColumnType("NVARCHAR(15)")
               .IsRequired();

        builder.ToTable("Currencies");
    }
}
