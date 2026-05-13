
namespace Application.Features.Management.ProductGroups.Commands.UpdateProductGroup;
public sealed record UpdateProductGroupCommand(long ProductId, Guid? BrandId, long? CategoryId, string? Title, string? Description,
    bool? IsSerialized, Dictionary<string, string>? Attributes)
: IRequest<Result<Updated>>;

public static class UpdateProductCommandExtensions
{
    public static bool HasChanges(this UpdateProductGroupCommand command)
    {
        if (command.Attributes is null && command.IsSerialized is null && command.Title is null && command.BrandId is null && command.CategoryId is null
            && command.Description is null)
            return false;

        return true;
    }
}
