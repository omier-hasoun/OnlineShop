using Domain.Customers;
using Infrastructure.Configurations;

namespace Api.Services;

public class UserContext(IHttpContextAccessor context) : IUserContext
{
    public UserId Id
    {
        get
        {
            return Guid.Parse("019DD034-8F6F-7F1B-8F7C-FBE35EF82935");
        }
    }


        
}
