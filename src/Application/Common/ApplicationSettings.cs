
namespace Application.Common;

public sealed class ApplicationSettings
{
    public ApplicationSettings(string? baseUrl, string? businessName)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(baseUrl);
        ArgumentNullException.ThrowIfNullOrEmpty(businessName);

        BaseUrl = baseUrl;
        BusinessName = businessName;
    }

    public readonly string BaseUrl;
    public readonly string BusinessName;

}
