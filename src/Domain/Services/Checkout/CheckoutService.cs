
using Domain.Inventories;
using static Domain.DomainErrors;

namespace Domain.Services.Checkout;

public sealed class CheckoutService
{
    private readonly ShippingCostCalculator _shippingCostCalculator;

    public CheckoutService(ShippingCostCalculator shippingCostCalculator)
    {
        _shippingCostCalculator = shippingCostCalculator;
    }
    public Result<Order> PlaceOrder(
        OrderId orderId,
        Guid? userId,
        GuestAccountId? guestId,
        string? ProviderPaymentId,

        IReadOnlyCollection<OrderLineEntities> lineDetails)
    {
        if (IsEmptyOrder(lineDetails))
        {
            return CheckoutErrors.OrderHasNoItems;
        }

        List<OrderLine> orderLines = new(lineDetails.Count);


        foreach (var l in lineDetails)
        {
            if (!CanBuyProduct(l.Product))
            {
                return CheckoutErrors.ProductNotPurchasbale;
            }

            if (!IsQuantityWithinLimit(l))
            {
                return CheckoutErrors.QuantityLimitExceeded;
            }

            var inventory = l.inventories?.FirstOrDefault(x => x.StockQuantity >= l.Quantity);// get first inventory that has enough stock for the order

            if (inventory is null)
            {
                return CheckoutErrors.QuantityForProductNotAvailable.WithParameters(l.Product.Id);
            }

            var remvoveQuantityResult = inventory.RemoveQuantity(l.Quantity);

            if (remvoveQuantityResult.Failed)
                return remvoveQuantityResult.TopError;


            var orderLineResult = OrderLine.Create(
                l.Id,
                orderId,
                l.Product.Id,
                l.Group.Title,
                l.Quantity,
                l.Product.CurrentPrice
            );

            if (orderLineResult.Failed)
            {
                return orderLineResult.Errors;
            }

            orderLines.Add(orderLineResult.Value);
        }

        var subTotal = Money.Create(orderLines.Sum(x => x.Total.Value));
        var shippingCost = _shippingCostCalculator.Calculate(subTotal);
        var total = shippingCost + subTotal;

        return Order.Create(orderId, userId, guestId, subTotal, total, shippingCost, ProviderPaymentId, orderLines);
    }

    private static bool IsEmptyOrder(IReadOnlyCollection<OrderLineEntities> lines) => lines is null || lines.Count == 0;

    private static bool CanBuyProduct(Product product) => product != null && product.IsPublished();

    private static bool IsQuantityWithinLimit(OrderLineEntities line)
    {
        if (line.Quantity < 1)
            return false;

        return line.Group.IsSerialized
            ? line.Quantity <= CheckoutRules.MaxQuantityForSerializedProductsPerOrder
            : line.Quantity <= CheckoutRules.MaxQuantityForNonSerializedProductsPerOrder;
    }
}
