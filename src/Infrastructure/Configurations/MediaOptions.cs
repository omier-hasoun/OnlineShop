
namespace Infrastructure.Configurations;

public sealed class MediaOptions
{
    public const string SectionName = "Media";

    public string BaseUrl { get; set; } = string.Empty;

    public ImageOptions Images { get; set; } = new();
}


public sealed class ImageOptions
{
    public ProductImageOptions Products { get; set; } = new();
}
public sealed class ProductImageOptions
{
    public string Original { get; set; } = string.Empty;

    public string Large { get; set; } = string.Empty;

    public string Medium { get; set; } = string.Empty;

    public string Small { get; set; } = string.Empty;
}
