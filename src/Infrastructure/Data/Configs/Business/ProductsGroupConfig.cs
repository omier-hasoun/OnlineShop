using Domain.Brands;
using Domain.Categories;
using Domain.Common.Rules;
using Domain.ProductsGroups;
using Infrastructure.Common.EfCore.ValueComparers;
using Infrastructure.Common.EfCore.ValueConverters;

namespace Infrastructure.Data.Configs.Business;

internal sealed class ProductsGroupConfig : BaseEntityConfig<ProductsGroup>
{

    public override void Configure(EntityTypeBuilder<ProductsGroup> builder)
    {
        base.Configure(builder);

        builder.Ignore(x => x.Attributes);


        builder.HasKey(e => e.Id)
               .IsClustered();

        builder.Property(x => x.Id)
               .HasConversion(
                   id => id.Value,
                   value => new ProductsGroupId(value)
               )
               .ValueGeneratedNever();

        builder.Property(x => x.Title)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(ProductGroupRules.MaxTitleLength)
               .IsRequired();

        builder.Property(x => x.Description)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(ProductGroupRules.MaxDescriptionLength)
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


        builder.ToTable("ProductsGroups", x =>
        {
            x.HasCheckConstraint("CK_Product_AverageRating", $"[AverageRating] between {ProductGroupRules.MinAverageRatingValue} and {ProductGroupRules.MaxAverageRatingValue}");

        });
    }
}
