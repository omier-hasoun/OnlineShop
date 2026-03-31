namespace Domain.Orders;

public enum OrderStatus
{
    Processing, // The order has been placed and is being processed.
    Pending, // The order is pending and waiting for some action (e.g., payment confirmation, inventory check).

    Confirmed, // The order has been confirmed and is ready for shipment.
    Shipped, // The order is on the way to the customer.
    Delivered, // The order has been delivered to the customer.

    Cancelled, // The order has been cancelled by the customer, Courier, system or Admin.
    Returned, // The order has been returned by the customer after delivery.
}
