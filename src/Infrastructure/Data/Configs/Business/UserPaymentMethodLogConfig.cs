
using Domain.UsersPaymentMethodsLogs;
using Infrastructure.Common.EfCore.ValueComparers;
using Infrastructure.Common.EfCore.ValueConverters;

namespace Infrastructure.Data.Configs.Business;

internal sealed class UserPaymentMethodLogConfig : BaseEntityConfig<UserPaymentMethodLog>
{
    public override void Configure(EntityTypeBuilder<UserPaymentMethodLog> builder)
    {
        base.Configure(builder);

        builder.Ignore(x => x.Details);


        builder.HasKey(e => e.Id)
               .IsClustered();

        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => new UserPaymentMethodLogId(value))
               .ValueGeneratedNever();

        builder.Property("_details")
               .HasColumnName("Details")
               .HasColumnType("NVARCHAR(MAX)")
               .HasConversion<JsonConverter<Dictionary<string, string>>>(new JsonDictionaryValueComparer())
               .IsRequired(false);

        builder.Property(x => x.ProviderBrandName)
               .HasColumnType("NVARCHAR(50)")
               .IsRequired();

        builder.Property(x => x.ProviderCustomerId)
               .HasColumnType("VARCHAR(100)")
               .IsRequired();

        builder.ToTable("UsersPaymentMethodsLogs");
    }
}
