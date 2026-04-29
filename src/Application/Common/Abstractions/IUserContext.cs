using Domain.Customers;

namespace Application.Common.Abstractions;

public interface IUserContext
{
    Guid Id { get; }
}
