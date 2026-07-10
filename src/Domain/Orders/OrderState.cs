
namespace Domain.Orders;

public enum OrderState
{
    Pending = 1,
    Confirmed = 2,
    RefundRequired,
    Processing,
    Delivered,
    Refunded,

}
