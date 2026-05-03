using Domain.Common.Rules;
using Domain.Customers;
using Domain.ProductReviews;
using Domain.Products;

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

        builder.HasOne<Product>()
               .WithMany()
               .HasForeignKey(x => x.ProductId)
               .IsRequired();

        builder.HasOne<Customer>()
               .WithMany()
               .HasForeignKey(x => x.CustomerId)
               .IsRequired();

        builder.ToTable("ProductReviews", x =>
        {
            x.HasCheckConstraint("CK_ProductReview_Rating", "Rating between 1 and 5");

        });
    }
}
