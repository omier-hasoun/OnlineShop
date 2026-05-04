using System.Text.Json;
using Domain.Brands;
using Domain.Categories;
using Domain.Common.Rules;
using Domain.Products;
using Infrastructure.Common.EfCore.ValueComparers;
using Infrastructure.Common.EfCore.ValueConverters;

namespace Infrastructure.Data.Configs.Business;

internal sealed class ProductConfig : BaseEntityConfig<Product>
{

    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Ignore(x => x.Attributes);


        builder.HasKey(e => e.Id)
               .IsClustered();

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

        builder.OwnsOne(x => x.AverageRating, lb =>
        {

            lb.Property(x => x.Value)
                .HasColumnType("FLOAT")
                .HasColumnName("AverageRating")
                .IsRequired();
        });

        builder.Property("_attributes")
               .HasColumnName("Attributes")
               .HasColumnType("NVARCHAR(MAX)")
               .HasConversion<JsonConverter<Dictionary<string, string>>>(new JsonDictionaryValueComparer())
               .IsRequired(false);

        builder.Property(x => x.Status)
               .HasColumnType("VARCHAR(50)")
               .HasConversion<string>()
               .IsRequired();

        builder.HasOne<Brand>()
               .WithMany()
               .HasForeignKey(x => x.BrandId)
               .IsRequired();

        builder.HasOne<Category>()
               .WithMany()
               .HasForeignKey(x => x.CategoryId)
               .IsRequired();


        builder.HasIndex(x => x.Title)
               .HasDatabaseName("IX_Product_Title")
               .IsUnique();


        builder.ToTable("Products", x =>
        {
            x.HasCheckConstraint("CK_Product_AverageRating", $"[AverageRating] between {ProductRules.MinAverageRatingValue} and {ProductRules.MaxAverageRatingValue}");

        });
    }
}
