

using Domain.Carts;

namespace Application.Features.Public.Carts.Commands.UpdateCartItem;

internal sealed class UpdateCartItemCommandHandler(IAppDbContext context) : IRequestHandler<UpdateCartItemCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UpdateCartItemCommand request, CancellationToken ct)
    {
        Cart? cart;
        if (request.CartIdentity.IsUser)
        {
            cart = await context.Carts.Include(x => x.Items).FirstOrDefaultAsync(x => x.UserId == request.CartIdentity.UserId, ct);
        }
        else
        {
            cart = await context.Carts.Include(x => x.Items).FirstOrDefaultAsync(x => x.GuestId == request.CartIdentity.GuestId, ct);
        }

        if (cart is null)
        {
            return ApplicationErrors.NotFound.Cart;
        }

        var result = cart.UpdateItem(request.ParsedCartItemId, request.Quantity);

        if (result.Failed)
            return result.Errors;

        await context.SaveAsync(ct);


        return Result.Updated;
    }
}
