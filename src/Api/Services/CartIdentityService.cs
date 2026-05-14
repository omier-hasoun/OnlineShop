using Application.Common.InternalModels;
using Domain.Common.ValueObjects;

namespace Api.Services;

public sealed class CartIdentityService(IHttpContextAccessor context, ICurrentUserService userService) : ICartIdentityService
{
    public CartIdentity GetCurrentIdentity()
    {
        var UserId = userService.Id;
        GuestAccountId? guestId = null;
       if(Guid.TryParse(context.HttpContext.Request.Cookies["guest_id"], out var value))
       {
            guestId = new GuestAccountId(value);
       }


        return new CartIdentity(UserId, guestId);
    }
}
