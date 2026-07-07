
namespace Domain.Orders;

public enum OrderState
{
    Pending = 1,
    Confirmed,
    RefundRequired,
    Processing,
    Delivered,
    Refunded,

}
