

namespace Application.Features.Management.Users.Commands.SetUserRole;

public sealed record SetUserRoleCommand(
    Guid CurrentUserId,
    Guid UserId,
    string Role 
) : IRequest<Result<Success>>;

