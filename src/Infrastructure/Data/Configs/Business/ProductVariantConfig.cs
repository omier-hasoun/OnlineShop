
using Domain.Common.EntitiesRules;
using Domain.Products;
using Domain.Products.ProductVariants;
using Infrastructure.Common.EfCore.ValueComparers;
using Infrastructure.Common.EfCore.ValueConverters;

namespace Infrastructure.Data.Configs.Business;

internal sealed class ProductVariantConfig : BaseEntityConfig<ProductVariant>
{
    public override void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        base.Configure(builder);

        builder.Ignore(x => x.Slug);
        builder.Ignore(x => x.Specifications);


        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => new ProductVariantId(value))
               .ValueGeneratedNever();

        builder.Property(x => x.Sku)
               .HasColumnType("VARCHAR")
               .HasMaxLength(ProductVariantRules.MaxSkuLength)
               .IsRequired();

        builder.Property(x => x.DiscountPercentage)
               .HasColumnType("TINYINT")
               .IsRequired();

        builder.Property(x => x.DiscountPrice)
               .IsRequired();

        builder.Property(x => x.OriginalPrice)
               .IsRequired();

        builder.Property("_specifications")
               .HasColumnName("Specifications")
               .HasColumnType("NVARCHAR(3000)")
               .HasConversion<JsonValueConverter<Dictionary<string, string>>>(new JsonDictionaryValueComparer())
               .IsRequired(false);

        builder.HasIndex(x => x.Sku)
               .IsUnique();

        builder.HasOne<Product>()
               .WithMany(x => x.Variants)
               .HasForeignKey(x => x.ProductId)
               .IsRequired();

        builder.ToTable("ProductVariants", x =>
        {
            x.HasCheckConstraint("CK_ProductVariant_DiscountPercentage", $"[DiscountPercentage] between {ProductVariantRules.MinDiscountPercentageValue} and {ProductVariantRules.MaxDiscountPercentageValue}");
            x.HasCheckConstraint("CK_ProductVariant_OriginalPrice", $"[OriginalPrice] between {ProductVariantRules.MinOriginalPriceValue} and {ProductVariantRules.MaxOriginalPriceValue}");
            x.HasCheckConstraint("CK_ProductVariant_DiscountPrice", $"[DiscountPrice] between {ProductVariantRules.MinDiscountPriceValue} and {ProductVariantRules.MaxDiscountPriceValue}");

        });
    }
}
