using Domain.Customers;

namespace Api.Services;

public class UserContext() : IUserContext
{
    public UserId Id => Guid.CreateVersion7();
}
