
namespace Domain.Common.EntitiesRules;

public static class ProductImageRules
{
    public const byte MinSortOrder = 1;
    public const byte MaxSortOrder = 50;

    public static readonly string[] AllowedExtensions = new[] { "jpg", "jpeg", "png", "gif", "bmp", "webp" };
}
