using Domain.Common;

namespace Application.Common.Abstractions;

public interface IUserContext
{
    UserId Id { get; }
}
