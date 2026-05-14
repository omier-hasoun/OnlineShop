
using Application.Common.InternalModels;

namespace Application.Common.Abstractions;

public interface ICartIdentityService
{
    CartIdentity GetCurrentIdentity();
}
