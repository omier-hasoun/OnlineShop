namespace Domain.Orders.OrderLines;

public enum OrderLineState
{
    Pending, // The order is pending and waiting for some action (e.g., payment confirmation, inventory check).
    Confirmed, // The order has been confirmed and is ready for shipment.

    Canceled, // The order has been canceled by the customer, Courier, system or Admin.

    Shipped, // The order is on the way to the customer.
    Delivered, // The order has been delivered to the customer.

    Returned, // customer returned all quantity of this order item after it was shipped or delivered.
    PartiallyReturned, // customer returned some of the full quantity of this order item after it was shipped or delivered.

}
