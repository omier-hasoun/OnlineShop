
using Domain.Customers;
using Domain.Customers.CustomerShippingAddresses;

namespace Infrastructure.Data.Configs.Business;

public sealed class CustomerShippingAddressConfig : BaseEntityConfig<CustomerShippingAddress>
{
    public override void Configure(EntityTypeBuilder<CustomerShippingAddress> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => new CustomerShippingAddressId(value))
               .ValueGeneratedNever();

        builder.Property(x => x.IsDefault)
               .IsRequired();

        builder.HasOne<Customer>()
               .WithMany(x => x.ShippingAddresses)
               .HasForeignKey(x => x.CustomerId)
               .IsRequired();

        builder.HasOne(x => x.Address)
               .WithOne()
               .HasForeignKey<CustomerShippingAddress>(x => x.AddressId)
               .IsRequired();

        builder.Navigation( x => x.Address)
               .AutoInclude();

        builder.HasIndex(x => x.CustomerId)
               .HasFilter("[IsDefault] = 1")// the goal here is that each user must have only one IsDefault = true Address record representing a default address, all other addresses for the user must be IsDefault = false
               .IsUnique()
               .HasDatabaseName("IX_Addresses_CustomerId");

        builder.ToTable("CustomerShippingAddresses");
    }
}
