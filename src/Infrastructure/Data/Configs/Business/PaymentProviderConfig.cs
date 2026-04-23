
using Domain.PaymentProviders;

namespace Infrastructure.Data.Configs.Business;

internal sealed class PaymentProviderConfig : BaseEntityConfig<PaymentProvider>
{
    public override void Configure(EntityTypeBuilder<PaymentProvider> builder)
    {
        base.Configure(builder);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => new PaymentProviderId(value))
               .ValueGeneratedNever();

        builder.Property(x => x.LogoUrl)
               .HasColumnType("NVARCHAR(255)")
               .IsRequired(false);

        builder.Property(x => x.BrandName)
               .HasColumnType("NVARCHAR(50)")
               .IsRequired();

        builder.Property(x => x.CompanyName)
               .HasColumnType("NVARCHAR(100)")
               .IsRequired();

        builder.ToTable("PaymentProviders");


    }
}
