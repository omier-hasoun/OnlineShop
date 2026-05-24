namespace Domain.ReturnItemRequests;

public enum ReturnItemRequestStatus
{
    // Initial State
    PendingArrival = 1,      // Replaces WaitingForProductArrival

    // Logistics
    Received,            // Replaces ProductArrived
    UnderReview,         // Keep - standard terminology

    // Fee/Action Required
    AwaitingPayment,     // Replaces AdditionalFeesRequired (more active)

    // Final Decisions
    Approved,            // Standard alternative to "Accepted"
    Rejected,            // Keep

    // Post-Decision Logistics
    ReturningToCustomer, // Replaces WillBeSentBack (clearer direction)
    ReturnPendingFees,   // Replaces WillBeSentBackForFees

    // Potential Missing Cases
    Canceled,           // User changed their mind
    ActionRequired,      // Generic state if you need more info from user
    Completed            // The return is fully processed and closed
}
