using Domain.ProductReviews;

namespace Infrastructure.Data.Configs.Business;

public sealed class ReviewConfig : BaseEntityConfig<ProductReview>
{
    public override void Configure(EntityTypeBuilder<ProductReview> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
               .ValueGeneratedOnAdd()
               .HasConversion(
                   id => id.Value,
                   value => new ProductReviewId(value)
               );

        builder.Property(x => x.Comment)
               .HasColumnType("NVARCHAR(128)")
               .IsRequired();

        builder.ToTable("Reviews", x =>
        {
            x.HasCheckConstraint("CK_Review_Rating", "Rating between 1 and 5");

        });
    }
}
