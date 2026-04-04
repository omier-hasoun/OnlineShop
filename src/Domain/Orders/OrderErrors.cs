
namespace Domain.Orders;

public static class OrderErrors
{
    public static Error OrderItemsCountOutOfRange => 
        Error.Validation("Order.OrderItemsCount.OutOfRange", $"Order must contain between {OrderRules.MinOrderItemsCount} and {OrderRules.MaxOrderItemsCount} items.");
    public static Error ShippingFeesOutOfRange =>
        Error.Validation("Order.ShippingFees.OutOfRange", $"ShippingFees must be between {OrderRules.MaxShippingFeesValue} and {OrderRules.MaxShippingFeesValue} USD.");
    public static Error TotalItemsPriceOutOfRange =>
        Error.Validation("Order.TotalItemsPrice.OutOfRange", $"TotalItemsPrice must be between {OrderRules.MinTotalItemsPriceValue} and {OrderRules.MaxTotalItemsPriceValue} USD.");
}
