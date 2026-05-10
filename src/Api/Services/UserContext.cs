using Api.Common.Exceptions;
 

namespace Api.Services;

public class UserContext(IHttpContextAccessor context) : IUserContext
{
    public System.Guid Id
    {
        get
        {
            if(System.Guid.TryParse(context.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var Id))
            {
                return Id;
            }
            return System.Guid.Parse("10000000-0000-0000-0000-000000000001");
            //throw new HttpRequestMissingUserIdException();
        }
    }


        
}
