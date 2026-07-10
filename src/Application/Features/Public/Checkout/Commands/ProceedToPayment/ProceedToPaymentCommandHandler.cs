
using Application.Features.Public.Checkout.Dtos;
using Domain.Common.ValueObjects;
using Domain.Orders.OrderLines;
using Domain.Services.Checkout;
#pragma warning disable IDE0042

namespace Application.Features.Public.Checkout.Commands.ProceedToPayment;

internal sealed class ProceedToPaymentCommandHandler(
    IAppDbContext context,
    CheckoutService checkout,
    IProductThumbnailUrlProvider thumbnailUrlProvider,
    IPaymentGateway paymentGateway,
    IIdGenerator<OrderId> orderIdGen,
    IIdGenerator<OrderLineId> orderLineIdGen,
    IApplicationUrlProvider appUrlProvider) : IRequestHandler<ProceedToPaymentCommand, Result<string>>
{
    private const string _defaultCurrency = "usd";

    public async Task<Result<string>> Handle(ProceedToPaymentCommand request, CancellationToken ct)
    {
        var identity = request.UserIdentity;

        var orderId = orderIdGen.NewId();

        string successUrl = appUrlProvider.GetPaymentSuccessUrl("dd");// should change later

        string cancelUrl = appUrlProvider.GetPaymentFailedUrl("dd");// should change later

        await CancelOrderPaymentProcessAndOrderIfExists(identity, ct);

        var items = await GetCartDetails(identity, ct);

        if (items.Count == 0)
        {
            return ApplicationErrors.Validation.CartIsEmpty;
        }


        items.ForEach(x => x.Id = orderLineIdGen.NewId());// giving ids for the order lines


        var orderResult = checkout.PlaceOrder(orderId, identity.UserId, identity.GuestId, null, items);

        if (orderResult.Failed)
            return orderResult.Errors;

        var order = orderResult.Value;


        var orderLinesDetails = items.Select(x =>
        {
            string? imageFileName = x.Product.Images.FirstOrDefault()?.FileName;

            string? thumbnailUrl = null;

            if (imageFileName != null)
                thumbnailUrl = thumbnailUrlProvider.GetUrl(imageFileName, ProductThumbnailSize.Small);

            return new OrderLineDetailsDto(x.Product.Id.Value, thumbnailUrl, x.Product.CurrentPrice.ToCents(), x.Group.Title, x.Quantity);
        })
        .ToList();


        string? userEmail = await context.Users.Where(x => x.Id == identity.UserId)
                                               .Select(x => x.NormalizedUserName)
                                               .FirstOrDefaultAsync(ct);
        bool collectUserEmail = false;

        if (!string.IsNullOrEmpty(userEmail))
        {
            order.SetEmailAddress(EmailAddress.Create(userEmail).Value);
        }
        else
        {
            collectUserEmail = true;
        }

        var orderDetails = new OrderDetailsDto(orderId.ToString(), _defaultCurrency, successUrl, cancelUrl, order.ShippingCost.ToCents(), orderLinesDetails, collectUserEmail);

        var PaymentProcess = await paymentGateway.StartPaymentProcessAsync(orderDetails, ct);

        if (PaymentProcess.SessionUrl is null || PaymentProcess.SessionId is null)
        {
            return ApplicationErrors.Unexpected.CheckoutFailed;
        }

        order.SetProviderPaymentId(PaymentProcess.SessionId);

        context.Orders.Add(order);

        await context.SaveAsync(ct);

        return PaymentProcess.SessionUrl;
    }

    private async Task CancelOrderPaymentProcessAndOrderIfExists(UserIdentity identity, CancellationToken ct)
    {
        var Order = await context.Orders.UserAbandonedOrderQuery(identity)
                                        .FirstOrDefaultAsync(ct);

        if (Order != null)
        {
            await paymentGateway.CancelPaymentProcess(Order.ProviderReferenceId!, CancellationToken.None);
            context.Orders.Remove(Order);
        }
    }

    private async Task<List<ItemInfo>> GetCartDetails(UserIdentity identity, CancellationToken ct)
    {
        return await context.CartItems
                     .Join(context.Carts.GetUserCartQuery(identity), ci => ci.CartId, c => c.Id, (CartItem, Cart) => new { CartItem })
                     .Join(context.Products, x => x.CartItem.ProductId, p => p.Id, (CartItem, Product) => new { c = CartItem.CartItem, Product })
                     .Join(context.ProductGroups, x => x.Product.ProductGroupId, g => g.Id, (cp, g) => new { CartItem = cp.c, Product = cp.Product, Group = g })
                     .Select(x => new ItemInfo(
                         x.Product,
                         x.Product.Inventories.ToList(),
                         x.Group,
                         x.CartItem.Quantity
                     ))
                     .ToListAsync(ct);
    }

}
