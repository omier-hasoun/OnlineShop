
using Application.Entities;
using Domain.CustomerShippingAddresses;

namespace Infrastructure.Data.Configs.Business;

internal sealed class ShippingAddressConfig : BaseEntityConfig<ShippingAddress>
{
    public override void Configure(EntityTypeBuilder<ShippingAddress> builder)
    {

        builder.HasKey(e => e.Id)
               .IsClustered();

        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => new ShippingAddressId(value))
               .ValueGeneratedNever();

        builder.Property(x => x.IsDefault)
               .IsRequired();

        builder.HasOne<AppUser>()
               .WithMany(x => x.ShippingAddresses)
               .HasForeignKey(x => x.UserId)
               .IsRequired();

        builder.HasOne(x => x.Address)
               .WithOne()
               .HasForeignKey<ShippingAddress>(x => x.AddressId)
               .IsRequired();

        builder.Navigation( x => x.Address)
               .AutoInclude();

        builder.HasIndex(x => x.UserId)
               .HasFilter("[IsDefault] = 1")// the goal here is that each user must have only one IsDefault = true Address record representing a default address, all other addresses for the user must be IsDefault = false
               .IsUnique()
               .HasDatabaseName("IX_ShippingAddresses_UserId");

        builder.ToTable("ShippingAddresses");
    }
}
