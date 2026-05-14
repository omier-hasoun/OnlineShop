
using Domain.Common.ValueObjects;

namespace Application.Common.InternalModels;

public record CartIdentity(Guid? UserId, GuestAccountId? GuestId)
{
    public bool IsUser => UserId.HasValue;
}
