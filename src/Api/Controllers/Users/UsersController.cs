

using Application.Features.Management.Users.Commands.SetUserRole;

namespace Api.Controllers.Users;

[Route("api/users")]
[Authorize(Roles = "Admin")]
public sealed class UsersController(ISender mediator, ICurrentUserService currentUserService) : ApiController
{
    [HttpPost("roles/{userId:Guid}")]
    public async Task<IActionResult> SetUserRole(Guid userId, [FromQuery] SetUserRolesRequest request, CancellationToken ct)
    {
        
        var result = await mediator.Send(new SetUserRoleCommand(currentUserService.GetUserId()!.Value, userId, request.Role), ct);

        return result.Match((response) => Ok(response), Problem);
    }

}
