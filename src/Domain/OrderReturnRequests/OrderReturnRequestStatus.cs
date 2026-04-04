namespace Domain.OrderReturnRequests;

public enum OrderReturnRequestStatus
{
    Accepted,
    Rejected,
    Pending,
    WaitingForCustomerAnswer,
    UnderReview,
}
