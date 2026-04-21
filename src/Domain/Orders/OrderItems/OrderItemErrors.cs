
namespace Domain.Orders.OrderItems;

public static class OrderItemErrors
{
    public static Error SerialNumbersDoNotMatchQuantity =>
        Error.Validation("OrderItem.SerialNumbers.DoNotMatchQuantity", $"Serial numbers do not match the quantity of your order item.");

    public static Error InvalidReturnQuantity =>
        Error.Validation("OrderItem.SerialNumbers.DoNotMatchQuantity", $"Serial numbers do not match the quantity of your order item.");

    public static Result<Success> CannotReturn { get; internal set; }
    public static Result<Success> CannotCancel { get; internal set; }
    public static Result<Success> CannotConfirm { get; internal set; }
}
