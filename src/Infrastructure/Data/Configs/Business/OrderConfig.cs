
using Application.Entities;
using Domain.Common.ValueObjects;
using Domain.Orders;

namespace Infrastructure.Data.Configs.Business;

internal sealed class OrderConfig : BaseEntityConfig<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {

        builder.HasKey(e => e.Id)
               .IsClustered();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.GuestId)
               .HasConversion(x => x!.Value.Value, value => new GuestAccountId(value))
               .ValueGeneratedNever()
               .IsRequired(false);

        builder.Property(x => x.Id)
               .ValueGeneratedNever()
               .HasConversion(
                   id => id.Value,
                   value => new OrderId(value)
               );

        builder.Property(x => x.SubTotal)
               .IsRequired();

        builder.Property(x => x.TaxAmount)
               .IsRequired();

        builder.Property(x => x.ShippingCost)
               .IsRequired();

        builder.Property(x => x.Email)
               .IsRequired(false);

        builder.HasIndex(x => x.GuestId)
               .HasFilter("[GuestId] is not null")
               .HasDatabaseName("IX_Orders_GuestId");


        builder.HasIndex(x => x.UserId)
               .HasFilter("[UserId] is not null")
               .HasDatabaseName("IX_Orders_UserId");


        builder.OwnsOne(x => x.ShippingAddress, nb =>
        {
            nb.ToJson();

            nb.Property(x => x.FullName);
            nb.Property(x => x.PhoneNumber);
            nb.Property(x => x.Country);
            nb.Property(x => x.City);
            nb.Property(x => x.PostalCode);
            nb.Property(x => x.AddressLine1);
            nb.Property(x => x.HouseNo);
            nb.Property(x => x.AddressLine2);
            nb.Property(x => x.StateProvince);
            nb.Property(x => x.Notes);
        });

        builder.OwnsOne(x => x.BillingAddress, nb =>
        {
            nb.ToJson();

            nb.Property(x => x.FullName);
            nb.Property(x => x.PhoneNumber);
            nb.Property(x => x.Country);
            nb.Property(x => x.City);
            nb.Property(x => x.PostalCode);
            nb.Property(x => x.AddressLine1);
            nb.Property(x => x.HouseNo);
            nb.Property(x => x.AddressLine2);
            nb.Property(x => x.StateProvince);
            nb.Property(x => x.Notes);
        });

        builder.HasOne<AppUser>()
               .WithMany()
               .HasForeignKey(x => x.UserId)
               .IsRequired(false);

        builder.ToTable("Orders");
    }
}
