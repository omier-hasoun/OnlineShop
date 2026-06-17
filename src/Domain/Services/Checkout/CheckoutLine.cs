namespace Domain.Services.Checkout;

public sealed record CheckoutLine(short Quantity, Product Product, ProductGroup ProductGroup)
{
}
