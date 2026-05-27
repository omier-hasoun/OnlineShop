using Domain.Brands;
using Domain.Categories;
using Domain.Common.Rules;
using Domain.ProductGroups;
using Domain.ProductGroups.Products;
using Infrastructure.Common.EfCore.ValueComparers;
using Infrastructure.Common.EfCore.ValueConverters;

namespace Infrastructure.Data.Configs.Business;

internal sealed class ProductGroupsConfig : BaseEntityConfig<ProductGroup>
{

    public override void Configure(EntityTypeBuilder<ProductGroup> builder)
    {
        base.Configure(builder);

        builder.Ignore(x => x.Attributes);


        builder.HasKey(e => e.Id)
               .IsClustered();

        builder.Property(x => x.Id)
               .HasConversion(
                   id => id.Value,
                   value => new ProductGroupId(value)
               )
               .ValueGeneratedNever();

        builder.Property(x => x.Title)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(ProductGroupRules.MaxTitleLength)
               .IsRequired();

        builder.Property(x => x.NormalizedTitle)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(ProductGroupRules.MaxTitleLength)
               .IsRequired();

        builder.Property(x => x.BrandName)
               .HasColumnType("VARCHAR(100)")
               .IsRequired();

        builder.Property(x => x.CategoryName)
               .HasColumnType("VARCHAR(100)")
               .IsRequired();

        builder.Property(x => x.Description)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(ProductGroupRules.MaxDescriptionLength)
               .IsRequired();

        builder.OwnsOne(x => x.AverageRating, lb =>
        {

            lb.Property(x => x.Value)
                .HasColumnType("DECIMAL(9,4)")
                .HasColumnName("AverageRating")
                .IsRequired();
        });

        builder.Property("_attributes")
               .HasColumnName("Attributes")
               .HasColumnType("NVARCHAR(MAX)")
               .HasConversion<JsonConverter<Dictionary<string, string>>>(new JsonDictionaryValueComparer())
               .IsRequired(false);

        builder.Property(x => x.Status)
               .HasConversion<int>()
               .IsRequired();

        builder.HasOne<Brand>()
               .WithMany()
               .HasForeignKey(x => x.BrandId)
               .IsRequired();

        builder.HasOne<Category>()
               .WithMany()
               .HasForeignKey(x => x.CategoryId)
               .IsRequired();

        builder.HasOne(x => x.FeaturedProduct)
               .WithMany()
               .HasForeignKey(x => x.FeaturedProductId)
               .IsRequired(false);

        builder.HasIndex(x => x.FeaturedProductId)
               .IsUnique()
               .HasDatabaseName("UX_ProductGroup_FeaturedProductId");

        builder.HasIndex(x => new
        {
            x.Status,
            x.NormalizedTitle
        })
        .HasDatabaseName("IX_ProductGroups_Search")
        .HasFilter($"[{nameof(ProductGroup.FeaturedProductId)}] IS NOT NULL")
        .IncludeProperties(x => new
        {
            x.Id,
            x.FeaturedProductId,
            x.Title,
            x.BrandName
        }); ;

        builder.ToTable("ProductGroups", x =>
        {
            x.HasCheckConstraint("CK_ProductGroups_AverageRating", $"[AverageRating] between {ProductGroupRules.MinAverageRatingValue} and {ProductGroupRules.MaxAverageRatingValue}");

        });
    }
}
