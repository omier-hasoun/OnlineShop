
namespace Application.Features.Products;

internal static class ProductApplicationRules
{
    public static readonly string[] AllowedImageMediaTypes =
    {
        "image/jpeg",
        "image/png",
        "image/gif",
        "image/bmp",
        "image/webp"
    };

    public static readonly string AllowedImageExtensions = "jpg, jpeg, png, gif, bmb, webp";

    public const int MaxImageSizeBytes = 8_388_608;
    public const byte MaxImageSizeMb = 8;

}
