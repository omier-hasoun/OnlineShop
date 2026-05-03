
using System.Net.Mime;

namespace Application;

public static class ApplicationRules
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

        public const int MaxImageSizeForProducts = 10 * 1024 * 1024;//10 Mb

        public const short MinWidth = 800;
        public const short MinHeight = 800;


    }

}
