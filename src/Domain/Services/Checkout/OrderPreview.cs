namespace Domain.Services.Checkout;

public sealed record OrderPreview(decimal Total, decimal SubTotal, decimal ShippingCost, List<OrderLinePreview> OrderLines)
{
}
