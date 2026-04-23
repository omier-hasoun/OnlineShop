using System.Text.Json;
using Domain.Brands;
using Domain.Categories;
using Domain.Common.EntitiesRules;
using Domain.Products;
using Infrastructure.Common.EfCore.ValueComparers;
using Infrastructure.Common.EfCore.ValueConverters;

namespace Infrastructure.Data.Configs.Business;

public sealed class ProductConfig : BaseEntityConfig<Product>
{

    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Ignore(x => x.Attributes);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(
                   id => id.Value,
                   value => new ProductId(value)
               )
               .ValueGeneratedNever();

        builder.Property(x => x.Title)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(ProductRules.MaxTitleLength)
               .IsRequired();

        builder.Property(x => x.Description)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(ProductRules.MaxDescriptionLength)
               .IsRequired();

        builder.Property(x => x.AverageRating)
               .HasColumnType("FLOAT")
               .IsRequired();


        builder.Property(x => x.DefaultDiscountPrice)
               .IsRequired();

        builder.Property(x => x.DefaultOriginalPrice)
               .IsRequired();

        builder.Property(x => x.DefaultDiscountPercentage)
               .HasColumnType("TINYINT")
               .IsRequired();

        builder.Property(x => x.MaxQuantityPerCustomer)
               .HasColumnType("SMALLINT")
               .IsRequired();

        builder.Property("_attributes")
               .HasColumnName("Attributes")
               .HasColumnType("NVARCHAR(3000)")
               .HasConversion<JsonValueConverter<List<string>>>(new JsonListValueComparer())
               .IsRequired(false);

        builder.HasOne<Brand>()
               .WithMany()
               .HasForeignKey(x => x.BrandId)
               .IsRequired();

        builder.HasOne<Category>()
               .WithMany()
               .HasForeignKey(x => x.CategoryId)
               .IsRequired();

        builder.ToTable("Products", x =>
        {
            x.HasCheckConstraint("CK_Product_AverageRating", $"[AverageRating] between {ProductRules.MinAverageRatingValue} and {ProductRules.MaxAverageRatingValue}");
            x.HasCheckConstraint("CK_Product_MaxQuantityPerCustomer", $"[MaxQuantityPerCustomer]  between {ProductRules.MinValueOf_MaxQuantityPerCustomer} and {ProductRules.MaxValueOf_MaxQuantityPerCustomer}");

        });
    }
}
