using Application.Common.Configurations;
using Application.Common.Extensions;
using Application.Features.Public.Checkout.Dtos;
using Domain.Orders.OrderLines;
using Domain.Services.Checkout;
#pragma warning disable IDE0042
namespace Application.Features.Public.Checkout.Commands.ProceedToPayment;

internal sealed class ProceedToPaymentCommandHandler(
    IAppDbContext context,
    CheckoutService checkout,
    IPaymentGateway paymentGateway,
    IOptions<ProductImagePathOptions> opt,
    IIdGenerator<OrderId> orderIdGen,
    IIdGenerator<OrderLineId> orderLineIdGen,
    ApplicationSettings settings) : IRequestHandler<ProceedToPaymentCommand, Result<string>>
{
    private const string _defaultCurrency = "usd";

    public async Task<Result<string>> Handle(ProceedToPaymentCommand request, CancellationToken ct)
    {
        var identity = request.UserIdentity;

        var abandonedOrder = await context.Orders.UserAbandonedOrderQuery(identity)
                                                  .FirstOrDefaultAsync(ct);

        if (abandonedOrder != null)
        {
           await paymentGateway.CancelPaymentProcess(abandonedOrder!.ProviderReferenceId!, CancellationToken.None);
           context.Orders.Remove(abandonedOrder);
        }


        var items = await context.CartItems
                                .Join(context.Carts.UserCartQuery(identity), ci => ci.CartId, c => c.Id, (CartItem, Cart) => new { CartItem } )
                                .Join(context.Products, x => x.CartItem.ProductId, p => p.Id, (CartItem, Product) => new { c = CartItem.CartItem, Product })
                                .Join(context.ProductGroups, x => x.Product.ProductGroupId, g => g.Id, (cp, g) => new { CartItem = cp.c, Product = cp.Product, Group = g })
                                .Select(x => new
                                {
                                    x.CartItem.Quantity,
                                    x.Product,
                                    x.Group,
                                    x.Product.Inventories
                                }
                                )
                                .ToListAsync(ct);

        if (items.Count == 0)
        {
            return ApplicationErrors.Validation.CartIsEmpty;
        }

        var orderId = orderIdGen.NewId();

        var lineDetails = items.Select(x => new OrderLineEntities(orderLineIdGen.NewId(), x.Product, x.Inventories, x.Group, x.Quantity))
                               .ToList();

        var orderResult = checkout.PlaceOrder(orderId, identity.UserId, identity.GuestId, null,  lineDetails);

        if (orderResult.Failed)
            return orderResult.Errors;

        var order = orderResult.Value;
        var productThumnailUrl = Path.Combine(settings.BaseUrl, opt.Value.Images_200x200);

        var orderLinesDetails = items.Select(x =>
        {
            string? imageFileName = x.Product.Images.FirstOrDefault()?.FileName;

            string? thumbnailUrl = null;

            if (imageFileName != null)
                thumbnailUrl = Path.Combine(productThumnailUrl, imageFileName);

            return new OrderLineDetailsDto(x.Product.Id.Value, thumbnailUrl, x.Product.CurrentPrice.ToCents(), x.Group.Title, x.Quantity);
        }).ToList();

        var successUrl = Path.Combine(settings.OrderPaymentSucceededUrl, identity.IsUser ? identity.UserId.ToString()! : identity.GuestId!.ToString()!);
        var failUrl = settings.OrderPaymentFailedUrl;

        var checkoutDetails = new OrderDetailsDto(orderId.ToString(), _defaultCurrency, successUrl, failUrl, order.ShippingCost.ToCents(), orderLinesDetails);

        var response = await paymentGateway.StartPaymentProcessAsync(checkoutDetails, ct);

        if(response.SessionUrl is null || response.SessionId is null)
        {
            return ApplicationErrors.Unexpected.CheckoutFailed;
        }

        order.SetProviderPaymentId(response.SessionId);

        context.Orders.Add(order);

        await context.SaveAsync(ct);

        return response.SessionUrl;
    }

}
