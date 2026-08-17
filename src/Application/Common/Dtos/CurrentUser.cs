using Domain.Common.ValueObjects;

namespace Application.Common.Dtos;

public record CurrentUser(Guid? UserId, GuestAccountId? GuestId)
{
    public bool IsUser => UserId.HasValue;
}
