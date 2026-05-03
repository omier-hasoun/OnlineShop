
using Domain.Customers;
using Infrastructure.Common.EfCore.ValueConverters;

namespace Infrastructure.Data.Configs.Business;

internal sealed class CustomerConfig : BaseEntityConfig<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);


        builder.HasKey(e => e.Id)
               .IsClustered();

        builder.Property(x => x.Id)
               .HasConversion<CustomerIdConverter>()
               .ValueGeneratedNever();

        builder.HasOne<AppUser>()
               .WithOne()
               .HasForeignKey<Customer>(x => x.UserId);
               

        builder.ToTable("Customers");

    }

}
