using Domain.Common.ValueObjects;

namespace Application.Common.Dtos;

public record UserIdentity(Guid? UserId, GuestAccountId? GuestId)
{
    public bool IsUser => UserId.HasValue;
}
