using Domain.Common.ValueObjects;

namespace Api.Services;

public sealed class CartIdentityService(IHttpContextAccessor accessor, ICurrentUserService userService) : ICartIdentityService
{
    public CartIdentity GetCurrentIdentity()
    {
        var UserId = userService.Id;
        GuestAccountId? guestId = null;

        if(UserId is null)
        {
            if (Guid.TryParse(accessor.HttpContext!.Request.Cookies["guest_id"], out var value))
            {
                guestId = new GuestAccountId(value);
            }
            else
            {
                throw new InvalidOperationException("Couldn't identify the request sender");
            }
        }

        return new CartIdentity(UserId, guestId);
    }
}
