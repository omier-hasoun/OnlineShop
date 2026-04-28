
using System.Net.Mime;

namespace Application;

public class ApplicationRules
{
    public static class Uploads
    {
        public static readonly string[] AllowedImageMediaTypesList = {

            MediaTypeNames.Image.Jpeg,
            MediaTypeNames.Image.Bmp,
            MediaTypeNames.Image.Gif,
            MediaTypeNames.Image.Png,
            MediaTypeNames.Image.Webp,
        };

            public static readonly string[] AllowedImageExtensionsList = { "jpg", "jpeg", "png", "webp", "gif", "bmb" };

            public static readonly string AllowedImageExtensions = string.Join(", ", AllowedImageExtensionsList);

            public const int MaxImageSizeForProducts = 8 * 1024 * 1024;//8 Mb
        }

}
