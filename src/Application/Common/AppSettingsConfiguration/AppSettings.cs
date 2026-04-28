namespace Application.Common.AppSettingsConfiguration;

public sealed record AppSettings
{
    private AppSettings()
    {

    }

    public string Key { get; private init; } = null!;
    public string Value { get; private init; } = null!;

}
