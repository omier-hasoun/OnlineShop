
namespace Shared.Helpers;

public static class FileHelper
{
    public static bool TryGetExtesnionFromMediaType(string mediaType,out string ext)
    {
        if(_extensions.TryGetValue(mediaType, out string? value) && value != null)
        {
            ext = value;
            return true;
        }
        ext = ".bin";
        return false;
    }

    public static string GetExtensionFromMediaType(string mediaType)
    {
        if(TryGetExtesnionFromMediaType(mediaType, out var ext))
        {
            return ext;
        }

        throw new InvalidOperationException("Couldn't get file extension");

    }

private static readonly Dictionary<string, string> _extensions = new()
{
    // JPEG
    ["image/jpeg"] = ".jpg",
    ["image/jpg"] = ".jpg",

    // PNG
    ["image/png"] = ".png",

    // WebP
    ["image/webp"] = ".webp",

    // AVIF
    ["image/avif"] = ".avif",

    // GIF
    ["image/gif"] = ".gif",

    // BMP
    ["image/bmp"] = ".bmp",
    ["image/x-bmp"] = ".bmp",
    ["image/x-ms-bmp"] = ".bmp",

    // TIFF
    ["image/tiff"] = ".tif",
    ["image/x-tiff"] = ".tif",

    // SVG
    ["image/svg+xml"] = ".svg",

    // ICO
    ["image/x-icon"] = ".ico",
    ["image/vnd.microsoft.icon"] = ".ico",

    // HEIC / HEIF
    ["image/heic"] = ".heic",
    ["image/heif"] = ".heif",

    // RAW
    ["image/x-adobe-dng"] = ".dng"
};

}
