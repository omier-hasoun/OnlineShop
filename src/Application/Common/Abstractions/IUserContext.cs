using Domain.Customers;

namespace Application.Common.Abstractions;

public interface IUserContext
{
    CustomerId Id { get; }
}
