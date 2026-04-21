namespace Domain.Orders.OrderItems;

public enum OrderItemStatus
{
    Pending, // The order is pending and waiting for some action (e.g., payment confirmation, inventory check).
    Confirmed, // The order has been confirmed and is ready for shipment.

    Shipped, // The order is on the way to the customer.
    Delivered, // The order has been delivered to the customer.

    Cancelled, // The order has been cancelled by the customer, Courier, system or Admin.
    Returned, // customer returned all quantity of this order item after it was shipped or delivered.
    PartiallyReturned, // customer returned some of the full quantity of this order item after it was shipped or delivered.

}
