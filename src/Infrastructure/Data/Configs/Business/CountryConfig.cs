
using Domain.Countries;
using Domain.Currencies;

namespace Infrastructure.Data.Configs.Business;

internal sealed class CountryConfig : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(x => x.Id)
               .IsClustered();

        builder.Property(x => x.Id)
               .HasColumnOrder(1)
               .ValueGeneratedNever();

        builder.Property(x => x.Code)
               .HasColumnOrder(2)
               .HasColumnType("CHAR(2)")
               .IsRequired();

        builder.Property(x => x.Name)
               .HasColumnOrder(3)
               .HasColumnType("VARCHAR(255)")
               .IsRequired();


        builder.Property(x => x.PhoneCode)
               .HasColumnOrder(4)
               .IsRequired();

        builder.HasOne<Currency>()
               .WithMany()
               .HasForeignKey(x => x.CurrencyCode)
               .IsRequired(false);

        builder.ToTable("Countries");

    }
}
