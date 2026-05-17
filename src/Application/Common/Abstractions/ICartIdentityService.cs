
using Application.Common.Dtos;

namespace Application.Common.Abstractions;

public interface ICartIdentityService
{
    CartIdentity GetCurrentIdentity();
}
