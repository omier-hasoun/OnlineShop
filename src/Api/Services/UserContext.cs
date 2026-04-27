using Api.Common.Exceptions;
using Domain.Customers;
using Infrastructure.Configurations;

namespace Api.Services;

public class UserContext(IHttpContextAccessor context) : IUserContext
{
    public CustomerId Id
    {
        get
        {
            if(Guid.TryParse(context.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var Id))
            {
                return Id;
            }
            throw new HttpRequestMissingUserIdException();
            //return Guid.Parse("019DD034-8F6F-7F1B-8F7C-FBE35EF82935");
        }
    }


        
}
