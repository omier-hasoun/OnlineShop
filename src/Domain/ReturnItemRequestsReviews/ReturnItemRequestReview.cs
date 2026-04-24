
using Domain.Common.ValueObjects;

namespace Domain.ReturnItemRequestsReviews;

public sealed class ReturnItemRequestReview : AggregateRoot<ReturnItemRequestId>, IFullAudited
{
    public ReturnItemRequestReview(ReturnItemRequestId Id, UserId createdBy, UserId lastModifiedBy, DateTime createdAt, DateTime lastModifiedAt, ReviewDecisionType decisionType, string decisionReason, Money additionalFees)
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

    public static Result<ReturnItemRequestReview> Create(ReturnItemRequestId Id, ReviewDecisionType decisionType, string decisionReason, Money additionalFees)
    {
        UserId createdBy = UserId.EmptyInstance;
        UserId lastModifiedBy = createdBy;

        DateTime createdAt = TimeService.UtcNow;
        DateTime lastModifiedAt = createdAt;


        return new ReturnItemRequestReview(Id, createdBy, lastModifiedBy, createdAt, lastModifiedAt, decisionType, decisionReason, additionalFees);
    }

    public UserId CreatedBy { get; set; }
    public UserId LastModifiedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }

    public ReviewDecisionType DecisionType { get; private set; }
    public string DecisionReason { get; private set; }
    public Money AdditionalFees { get; private set; }


}
