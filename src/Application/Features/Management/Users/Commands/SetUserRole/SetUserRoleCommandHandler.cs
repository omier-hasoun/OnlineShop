
using Application.Entities;

namespace Application.Features.Management.Users.Commands.SetUserRole;

internal sealed class SetUserRoleCommandHandler(UserManager<AppUser> userManager) : IRequestHandler<SetUserRoleCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(SetUserRoleCommand request, CancellationToken ct)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());

        if (user is null)
        {
            return ApplicationErrors.NotFound.User;
        }

        if (!AppRoleTypes.AssignableRoles.Contains(request.Role))
        {
            return ApplicationErrors.Validation.RoleInvalid.WithParameters(request.Role);
        }


        var currentRoles = await userManager.GetRolesAsync(user);

        var removeResult = userManager.RemoveFromRolesAsync(user, currentRoles);

        if (removeResult.IsFaulted)
        {
            return ApplicationErrors.Unexpected.OperationFailed;
        }

        var result = await userManager.AddToRoleAsync(user, request.Role);

        if (result.Succeeded)
            return Result.Success;

        

        return ApplicationErrors.Unexpected.OperationFailed;
            
    }
}
