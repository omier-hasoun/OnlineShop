
namespace Application.Common.Abstractions;

public interface ICurrentUserService
{
    string? GetCurrentUserEmail();
    CurrentUser GetCurrentIdentity();
    Guid? GetUserId();
}
