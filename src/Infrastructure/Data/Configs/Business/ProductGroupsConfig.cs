using Domain.Brands;
using Domain.Categories;
using Domain.Common.Rules;
using Domain.ProductGroups;
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


        builder.HasIndex(x => x.NormalizedTitle)
               .IsUnique()
               .HasFilter($"[Status] = {(int)ProductGroupState.Published}")
               .HasDatabaseName("UX_Product_NormalizedTitle_Published");


        builder.ToTable("ProductGroups", x =>
        {
            x.HasCheckConstraint("CK_ProductGroups_AverageRating", $"[AverageRating] between {ProductGroupRules.MinAverageRatingValue} and {ProductGroupRules.MaxAverageRatingValue}");

        });
    }
}
