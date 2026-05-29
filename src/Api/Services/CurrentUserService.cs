using Domain.Common.ValueObjects;

namespace Api.Services;

public sealed class CurrentUserService(IHttpContextAccessor context) : ICurrentUserService
{
    public UserIdentity GetCurrentIdentity()
    {

        GuestAccountId? guestId = null;
        Guid? userId = GetUserId();
        if (userId is null)
        {
            if (Guid.TryParse(context.HttpContext!.Request.Cookies["guest_id"], out var value))
            {
                guestId = new GuestAccountId(value);
            }
            else
            {
                throw new InvalidOperationException("Couldn't identify the request sender");
            }
        }

        return new UserIdentity(userId, guestId);
    }

    public Guid? GetUserId()
    {
        var isUser = Guid.TryParse(context.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId);

        return isUser ? userId : null;
    }
}
