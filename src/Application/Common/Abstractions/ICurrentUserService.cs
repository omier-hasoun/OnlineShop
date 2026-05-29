
using Application.Common.Dtos;

namespace Application.Common.Abstractions;

public interface ICurrentUserService
{
    UserIdentity GetCurrentIdentity();
    Guid? GetUserId();
}
