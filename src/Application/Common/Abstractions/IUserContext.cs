using Domain.Customers;

namespace Application.Common.Abstractions;

public interface IUserContext
{
    UserId Id { get; }
}
