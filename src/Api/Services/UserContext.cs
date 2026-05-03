using Api.Common.Exceptions;
using Domain.Customers;

namespace Api.Services;

public class UserContext(IHttpContextAccessor context) : IUserContext
{
    public Guid Id
    {
        get
        {
            if(Guid.TryParse(context.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var Id))
            {
                return Id;
            }
            return Guid.Parse("00000000-0000-0000-0000-000000000000");
            //throw new HttpRequestMissingUserIdException();
        }
    }


        
}
