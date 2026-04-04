

using Domain.Addresses;

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
               .IsRequired();

        builder.Property(x => x.AddressLine1)
               .HasColumnType("NVARCHAR")
               .IsRequired();

        builder.Property(x => x.PostalCode)
               .HasColumnType("NVARCHAR")
               .IsRequired();

        builder.ToTable("address");

    }
}
