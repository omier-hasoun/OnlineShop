
using Domain.Common.Rules;
using Domain.Products;
using Domain.Products.ProductVariants;
using Domain.Products.ValueObjects;
using Infrastructure.Common.EfCore.ValueComparers;
using Infrastructure.Common.EfCore.ValueConverters;

namespace Infrastructure.Data.Configs.Business;

internal sealed class ProductVariantConfig : BaseEntityConfig<ProductVariant>
{
    public override void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        base.Configure(builder);

        builder.Ignore(x => x.Specifications);


        builder.HasKey(e => e.Id)
               .IsClustered();

        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => new ProductVariantId(value))
               .ValueGeneratedNever();

        builder.Property(x => x.Sku)
               .HasColumnType("VARCHAR")
               .HasMaxLength(ProductVariantRules.MaxSkuLength)
               .IsRequired();

        builder.Property(x => x.Slug)
               .HasColumnType("VARCHAR(80)")
               .IsRequired();

        builder.Property(x => x.Barcode)
               .HasColumnType("VARCHAR(100)")
               .IsRequired();

        builder.Property(x => x.DiscountPercentage)
               .HasColumnType("TINYINT")
               .IsRequired(false);

        builder.Property(x => x.Status)
               .HasColumnType("VARCHAR(50)")
               .HasConversion<string>()
               .IsRequired();

        builder.Property(x => x.Price)
               .IsRequired();

        builder.Property(x => x.PriceBeforeDiscount)
               .IsRequired(false);

        builder.OwnsMany(x => x.Images, b =>
        {
            b.ToJson();
        
        });

        builder.Property("_specifications")
               .HasColumnName("Specifications")
               .HasColumnType("NVARCHAR(MAX)")
               .HasConversion<JsonConverter<Dictionary<string, string>>>(new JsonDictionaryValueComparer())
               .IsRequired(false);

        builder.HasOne<Product>()
               .WithMany(x => x.Variants)
               .HasForeignKey(x => x.ProductId)
               .IsRequired();

        builder.ToTable("ProductVariants", x =>
        {
            x.HasCheckConstraint("CK_ProductVariant_DiscountPercentage", $"[DiscountPercentage] between {ProductVariantRules.MinDiscountPercentageValue} and {ProductVariantRules.MaxDiscountPercentageValue}");
            x.HasCheckConstraint("CK_ProductVariant_Price", $"[Price] between {ProductVariantRules.MinPrice} and {ProductVariantRules.MaxPrice}");

        });
    }
}
