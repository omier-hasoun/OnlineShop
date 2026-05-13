using Application.Entities;
using Domain.Common.Rules;
 
using Domain.ProductReviews;
using Domain.ProductsGroups;

namespace Infrastructure.Data.Configs.Business;

internal sealed class ProductReviewConfig : BaseEntityConfig<ProductReview>
{
    public override void Configure(EntityTypeBuilder<ProductReview> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id)
               .IsClustered();

        builder.Property(x => x.Id)
               .HasConversion(
                   id => id.Value,
                   value => new ProductReviewId(value)
               )
               .ValueGeneratedNever();

        builder.Property(x => x.Title)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(ProductReviewRules.MaxTitleLength)
               .IsRequired();

        builder.Property(x => x.Comment)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(ProductReviewRules.MaxCommentLength)
               .IsRequired(false);

        builder.Property(x => x.Rating)
               .HasColumnType("TINYINT")
               .IsRequired();

        builder.HasOne<ProductsGroup>()
               .WithMany()
               .HasForeignKey(x => x.ProductsGroupId)
               .IsRequired();

        builder.HasOne<AppUser>()
               .WithMany()
               .HasForeignKey(x => x.UserId)
               .IsRequired();

        builder.ToTable("ProductReviews", x =>
        {
            x.HasCheckConstraint("CK_ProductReview_Rating", "[Rating] between 1 and 5");

        });
    }
}
