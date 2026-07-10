
namespace Infrastructure.Configurations;

public sealed class ApplicationUrlsOptions
{
    public const string SectionName = "ApplicationUrls";

    public string BaseUrl { get; set; } = string.Empty;

    public string ApiUrl { get; set; } = string.Empty;
}
