

using Domain.Countries;
using Domain.Countries.StateProvinces;

namespace Infrastructure.Data.Configs.Business;

internal sealed class StateProvinceConfig : IEntityTypeConfiguration<StateProvince>
{
    public void Configure(EntityTypeBuilder<StateProvince> builder)
    {
        builder.HasKey(x => x.Id)
               .IsClustered();

        builder.Property(x => x.Id)
               .HasColumnOrder(1)
               .ValueGeneratedNever();

        builder.Property(x => x.Name)
               .HasColumnOrder(2)
               .HasColumnType("NVARCHAR(255)");


        builder.HasOne<Country>()
               .WithMany()
               .HasForeignKey(x => x.CountryId)
               .IsRequired();

        builder.ToTable("StateProvinces");
    }
}
