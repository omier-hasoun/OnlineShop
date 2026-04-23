
using Domain.Customers;

namespace Infrastructure.Data.Configs.Business;

public sealed class CustomerShippingAddressConfig : BaseEntityConfig<CustomerShippingAddress>
{
    public override void Configure(EntityTypeBuilder<CustomerShippingAddress> builder)
    {
        builder.Property(x => x.IsDefault)
               .IsRequired();

        builder.HasOne<Customer>()
               .WithMany(x => x.ShippingAddresses)
               .HasForeignKey(x => x.CustomerId)
               .IsRequired();

        builder.HasIndex(x => x.CustomerId)
               .HasFilter("[IsDefault] = 1")// the goal here is that each user must have only one IsDefault = true Address record representing a default address, all other addresses for the user must be IsDefault = false
               .IsUnique()
               .HasDatabaseName("IX_Addresses_CustomerId");
    }
}
