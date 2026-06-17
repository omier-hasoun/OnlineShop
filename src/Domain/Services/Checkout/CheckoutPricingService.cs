using Domain.Services.Models;

namespace Domain.Services.Checkout;

public sealed class CheckoutPricingService
{
    public CheckoutPricingService(ShippingCostCalculator shippingCostCalculator)
    {
        _shippingCostCalculator = shippingCostCalculator;
    }


    private static readonly OrderPreview _emptyOrder = new OrderPreview(Money.Zero, Money.Zero, Money.Zero, new List<OrderLinePreview>(1));
    private readonly ShippingCostCalculator _shippingCostCalculator;

    public Result<OrderPreview> CalculateCheckoutLines(IReadOnlyCollection<CheckoutLine> lines)
    {
        if (lines is null || lines.Count == 0)
            return _emptyOrder;

        List<OrderLinePreview> orderLines = new(lines.Count);

        foreach (var l in lines)
        {
            if (!l.Product.IsPublished())
            {
                return CheckoutErrors.InvalidProduct;
            }

            var unitPrice = l.Product.CurrentPrice;
            var lineTotal = CalculateOrderLineTotal(unitPrice, l.Quantity);

            var orderLine = new OrderLinePreview(l.Product.Id, l.ProductGroup.Title, l.Quantity, l.Product.CurrentPrice, lineTotal);
            orderLines.Add(orderLine);
        }

        var subTotal = CalculateSubTotal(orderLines);
        var shippingCost = _shippingCostCalculator.Calculate(subTotal);
        var total = shippingCost + subTotal;

        return new OrderPreview(total, subTotal, shippingCost, orderLines);
    }

    private Money CalculateOrderLineTotal(Money price, short quantity)
    {
        return Money.Create(price.Value * (decimal)quantity);
    }

    private Money CalculateSubTotal(IReadOnlyCollection<OrderLinePreview> lines)
    {
        return Money.Create(lines.Sum(l => l.LineTotal.Value));
    }
}
