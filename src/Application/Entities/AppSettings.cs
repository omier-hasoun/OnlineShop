namespace Application.Entities;

public sealed class AppSettings
{
    private AppSettings()
    {

    }

    public string Key { get; private init; } = null!;
    public string Value { get; private init; } = null!;

}
