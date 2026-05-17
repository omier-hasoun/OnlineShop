using Domain.Common.ValueObjects;

namespace Application.Common.Dtos;

public record CartIdentity(Guid? UserId, GuestAccountId? GuestId)
{
    public bool IsUser => UserId.HasValue;
}
