
using Domain.Common.Entities.Addresses;
using Domain.Common.EntitiesRules;


namespace Infrastructure.Data.Configs.Business;

public sealed class AddressConfig : BaseEntityConfig<Address>
{
    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        base.Configure(builder);

        builder.HasKey(e => e.Id);

        builder.Property(x => x.Id)
               .HasConversion(
                   id => id.Value,
                   value => new AddressId(value)
               )
               .ValueGeneratedNever();

        builder.Property(x => x.City)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(AddressRules.MaxCityLength)
               .IsRequired();

        builder.Property(x => x.AddressLine1)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(AddressRules.MaxAddressLine1Length)
               .IsRequired();

        builder.Property(x => x.AddressLine2)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(AddressRules.MaxAddressLine2Length)
               .IsRequired(false);

        builder.Property(x => x.PostalCode)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(AddressRules.MaxPostalCodeLength)
               .IsRequired();

        builder.Property(x => x.CountryCode)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(AddressRules.MaxCountryCodeLength)
               .IsFixedLength()
               .IsRequired();

        builder.Property(x => x.FullName)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(AddressRules.MaxFullNameLength)
               .IsRequired();

        builder.Property(x => x.Notes)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(AddressRules.MaxNotesLength)
               .IsRequired(false);

        builder.Property(x => x.HouseNo)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(AddressRules.MaxHouseNoLength)
               .IsRequired();

        builder.Property(x => x.PhoneNumber)
               .HasColumnType("VARCHAR")
               .HasMaxLength(AddressRules.MaxPhoneNumberLength)
               .IsRequired();

        builder.Property(x => x.StateProvince)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(AddressRules.MaxStateProvinceLength)
               .IsRequired(false);

        builder.OwnsOne(x => x.GeoLocation, locationBuilder =>
        {
            locationBuilder.Property(l => l.Latitude)
                           .HasColumnType("DECIMAL(9,6)")
                           .IsRequired();

            locationBuilder.Property(l => l.Longitude)
                           .HasColumnType("DECIMAL(9,6)")
                           .IsRequired();
        });

        //builder.UseTphMappingStrategy();

        builder.ToTable("Addresses");

    }
}
