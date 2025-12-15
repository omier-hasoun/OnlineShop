namespace Infrastructure.Data.Configs.Business;

public sealed class CustomerAddressConfig : BaseEntityConfig<CustomerAddress>
{
    public override void Configure(EntityTypeBuilder<CustomerAddress> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id)
               .ValueGeneratedOnAdd()
               .HasConversion(
                   id => id.Value,
                   value => new CustomerAddressId(value)
               );

        builder.Property(x => x.Country)
               .HasColumnType("NVARCHAR(32)")
               .IsRequired();

        builder.Property(x => x.City)
               .HasColumnType("NVARCHAR(32)")
               .IsRequired();

        builder.Property(x => x.Street)
               .HasColumnType("NVARCHAR(64)")
               .IsRequired();

        builder.Property(x => x.Zipcode)
               .HasColumnType("VARCHAR(8)")
               .IsRequired();

        builder.HasOne<Customer>()
               .WithOne(x => x.Address)
               .HasForeignKey<CustomerAddress>(x => x.CustomerId)
               .IsRequired();

        builder.ToTable("CustomerAddresses");

    }
}
