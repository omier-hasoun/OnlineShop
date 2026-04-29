
using Domain.Common.ValueObjects;

namespace Domain.ReturnItemRequestsReviews;

public sealed class ReturnItemRequestReview : AggregateRoot<ReturnItemRequestId>, IFullAudited
{
    private ReturnItemRequestReview()
    {
        
    }
    public ReturnItemRequestReview(ReturnItemRequestId Id, Guid createdBy, Guid lastModifiedBy, DateTime createdAt, DateTime lastModifiedAt, ReviewDecisionType decisionType, string decisionReason, Money additionalFees)
        : base(Id)
    {
        CreatedBy = createdBy;
        LastModifiedBy = lastModifiedBy;
        CreatedAt = createdAt;
        LastModifiedAt = lastModifiedAt;
        DecisionType = decisionType;
        DecisionReason = decisionReason;
        AdditionalFees = additionalFees;
    }

    public static Result<ReturnItemRequestReview> Create(ReturnItemRequestId Id, ReviewDecisionType decisionType, string decisionReason, Money? additionalFees)
    {
        Guid createdBy = Guid.Empty;
        Guid lastModifiedBy = createdBy;

        DateTime createdAt = TimeService.UtcNow;
        DateTime lastModifiedAt = createdAt;

        additionalFees ??= Money.From(0).Value;

        return new ReturnItemRequestReview(Id, createdBy, lastModifiedBy, createdAt, lastModifiedAt, decisionType, decisionReason, additionalFees);
    }

    public Guid CreatedBy { get; set; }
    public Guid LastModifiedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }

    public ReviewDecisionType DecisionType { get; private set; }
    public string DecisionReason { get; private set; } = null!;
    public Money AdditionalFees { get; private set; } = null!;


}
