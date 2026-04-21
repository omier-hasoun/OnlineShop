
namespace Domain.AppSettings;

public readonly record struct AppSettingsId
{
    public string Value { get; init; }

    public static implicit operator string(AppSettingsId key) => key.Value;
    public static implicit operator AppSettingsId(string value) => new AppSettingsId(value);
    public AppSettingsId(string value)
    {
        if (ValidationHelper.IsNullOrContainsWhiteSpace(value)|| value.Length > 50)
            throw new ArgumentException("AppSettingsId is invalid.", nameof(value));

        Value = value;
    }
}
