
using Domain.Common.EntitiesRules;
using Domain.ReturnItemRequests;
using Domain.ReturnItemRequestsReviews;

namespace Infrastructure.Data.Configs.Business;

internal sealed class ReturnItemRequestReviewConfig : BaseEntityConfig<ReturnItemRequestReview>
{
    public override void Configure(EntityTypeBuilder<ReturnItemRequestReview> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever();

        builder.Property(x => x.DecisionType)
               .HasColumnType("VARCHAR(50)")
               .HasConversion<string>()
               .IsRequired();

        builder.Property(x => x.DecisionReason)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(ReturnItemRequestReviewRules.DecisionReasonMaxLength)
               .IsRequired();

        builder.HasOne<ReturnItemRequest>()
               .WithOne()
               .HasForeignKey<ReturnItemRequestReview>(x => x.Id)
               .IsRequired();


        builder.ToTable("ReturnItemRequestsReviews");
    }
}
