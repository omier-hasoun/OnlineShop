using Api.Common.Exceptions;
 

namespace Api.Services;

public sealed class CurrentUserContextService(IHttpContextAccessor context) : ICurrentUserService
{
    public Guid? Id
    {
        get
        {
            if(Guid.TryParse(context.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var Id))
            {
                return Id;
            }
            return null;
        }
    }


        
}
