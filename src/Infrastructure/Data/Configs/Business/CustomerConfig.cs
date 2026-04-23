
using Domain.Customers;

namespace Infrastructure.Data.Configs.Business;

internal sealed class CustomerConfig : BaseEntityConfig<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);

        builder.Ignore(x => x.UserId);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever();

        builder.ToTable("Customers");

    }

}
