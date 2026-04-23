
using Domain.Brands;
using Domain.Common.EntitiesRules;

namespace Infrastructure.Data.Configs.Business;

public sealed class BrandConfig : BaseEntityConfig<Brand>
{
    public override void Configure(EntityTypeBuilder<Brand> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => new BrandId(value))
               .ValueGeneratedNever();

        builder.Property(x => x.CompanyName)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(BrandRules.MaxCompanyNameLength)
               .IsRequired();

        builder.Property(x => x.Name)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(BrandRules.MaxNameLength)
               .IsRequired();

        builder.Property(x => x.SkuName)
               .HasColumnType("VARCHAR")
               .HasMaxLength(BrandRules.MaxSkuNameLength)
               .IsRequired();

        builder.Property(x => x.Description)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(BrandRules.MaxDescriptionLength)
               .IsRequired();

        builder.Property(x => x.LogoUrl)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(255)
               .IsRequired(false);


        builder.ToTable("Brands");
    }
}
