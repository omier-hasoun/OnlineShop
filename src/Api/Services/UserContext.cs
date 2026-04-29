using Api.Common.Exceptions;
using Domain.Customers;
using Infrastructure.Configurations;

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
            throw new HttpRequestMissingUserIdException();
        }
    }


        
}
