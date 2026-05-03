
using Domain.Common.Rules;
using Domain.ReturnItemRequests;
using Domain.ReturnItemRequestsReviews;

namespace Infrastructure.Data.Configs.Business;

internal sealed class ReturnItemRequestReviewConfig : BaseEntityConfig<ReturnItemRequestReview>
{
    public override void Configure(EntityTypeBuilder<ReturnItemRequestReview> builder)
    {
        base.Configure(builder);

        builder.HasKey(e => e.Id)
               .IsClustered();

        builder.Property(x => x.Id)
               .ValueGeneratedNever();

        builder.Property(x => x.DecisionType)
               .HasColumnType("VARCHAR(50)")
               .HasConversion<string>()
               .IsRequired();

        builder.Property(x => x.DecisionReason)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(255)
               .IsRequired();

        builder.HasOne<ReturnItemRequest>()
               .WithOne()
               .HasForeignKey<ReturnItemRequestReview>(x => x.Id)
               .IsRequired();


        builder.ToTable("ReturnItemRequestsReviews");
    }
}
