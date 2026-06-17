
using Domain.Carts;

namespace Application.Features.Public.Checkout.Commands.BeginCheckout;

internal sealed class BeginCheckoutCommandHandler(IAppDbContext context, ICheckoutProvider checkout) : IRequestHandler<BeginCheckoutCommand, Result<string>>
{
    public async Task<Result<string>> Handle(BeginCheckoutCommand request, CancellationToken ct)
    {
        var identity = request.UserIdentity;
        Cart? cart = null;
        string? referenceId = null;

        //var query = context.CartItems.Join(context.Products, c => c.ProductId, p => p.Id, (ci, p) => new { ci, p })
        //                             .Join(context.ProductGroups, x => x.p.ProductGroupId, pg => pg.Id, (item, pg) => new { item.ci, item.p, pg })
        //                             .Select(x => new OrderItemDetailsDto(x.pg.Title, x.p.CurrentPrice, x.p)




        //var projectionQuery = query.Join(context.Products)


        if (cart is null || cart.Items.Count == 0)
        {
            return ApplicationErrors.Validation.CartIsEmpty;
        }
        throw new NotImplementedException();
        ////var orderItemDetails = cart.Items.Select(x => new OrderItemDetailsDto(x.Product))
        //    }


        //var checkoutDto = new CheckoutSessionInfo(referenceId!, "omierhasoun@gmail.com", "usd",
        //    [new OrderItemDetailsDto("shit here we go again", 21.99m, 2199, 2)]);

        //var url = await checkout.BeginCheckout(checkoutDto, ct);

        //return url;
    }
}
