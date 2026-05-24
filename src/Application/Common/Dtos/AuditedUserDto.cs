
namespace Application.Common.Dtos;

public sealed record AuditedUserDto(Guid UserId, string UserName, DateTime TimeUtc);
