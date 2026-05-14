
namespace Domain.Common.Extensions;

internal static class GuidExtensions
{
    public static Result<Success> IsValidUserId(this Guid userId)
    {
        if (userId == default)
            return DomainErrors.UserIdInvalid;

        return Result.Success;
    }
}
