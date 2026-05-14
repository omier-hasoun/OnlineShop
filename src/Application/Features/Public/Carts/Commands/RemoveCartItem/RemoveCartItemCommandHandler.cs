
using Domain.Carts;

namespace Application.Features.Public.Carts.Commands.RemoveCartItem;

internal sealed class RemoveCartItemCommandHandler(IAppDbContext context) : IRequestHandler<RemoveCartItemCommand, Result<Deleted>>
{
    public async Task<Result<Deleted>> Handle(RemoveCartItemCommand request, CancellationToken ct)
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

        var result = cart.RemoveItem(request.ParsedCartItemId);

        if (result.Failed)
            return result.Errors;

        await context.SaveAsync(ct);

        return Result.Deleted;
    }
}
