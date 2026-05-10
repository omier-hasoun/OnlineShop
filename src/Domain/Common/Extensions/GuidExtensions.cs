
namespace Domain.Common.Extensions;

internal static class GuidExtensions
{
    public static Result<Success> IsValidUserId(this Guid userId)
    {
        if (userId == default || userId.Version != 7)
            return DomainErrors.UserIdInvalid;

        return Result.Success;
    }
}
