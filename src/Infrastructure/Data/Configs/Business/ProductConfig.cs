
using Domain.Common.Rules;
using Domain.ProductGroups;
using Domain.ProductGroups.Products;
using Domain.ProductGroups.ValueObjects;
using Infrastructure.Common.EfCore.ValueComparers;
using Infrastructure.Common.EfCore.ValueConverters;

namespace Infrastructure.Data.Configs.Business;

internal sealed class ProductConfig : BaseEntityConfig<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Ignore(x => x.Specifications);

        builder.HasKey(e => e.Id)
               .IsClustered();

        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => new ProductId(value))
               .ValueGeneratedNever();

        builder.Property(x => x.Sku)
               .HasColumnType("VARCHAR")
               .HasMaxLength(ProductRules.MaxSkuLength)
               .IsRequired();

        builder.Property(x => x.Slug)
               .HasColumnType("VARCHAR(80)")
               .IsRequired();

        builder.Property(x => x.BarCode)
               .HasColumnType("VARCHAR(50)")
               .IsRequired();

        builder.Property(x => x.DiscountPercentage)
               .HasColumnType("TINYINT")
               .IsRequired(false);

        builder.Property(x => x.Status)
               .HasConversion<int>()
               .IsRequired();

        builder.Property(x => x.OriginalPrice)
               .IsRequired();

        builder.Property(x => x.HasActiveDiscount)
               .IsRequired();

        builder.Property(x => x.PriceAfterDiscount)
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

        builder.HasOne<ProductGroup>()
               .WithMany(x => x.Products)
               .HasForeignKey(x => x.ProductGroupId)
               .IsRequired();

        builder.ToTable("Products", x =>
        {
            x.HasCheckConstraint("CK_Product_DiscountPercentage", $"[DiscountPercentage] between {ProductRules.MinDiscountPercentageValue} and {ProductRules.MaxDiscountPercentageValue}");
            x.HasCheckConstraint("CK_Product_Price", $"[OriginalPrice] between {ProductRules.MinPrice} and {ProductRules.MaxPrice}");

        });
    }
}
