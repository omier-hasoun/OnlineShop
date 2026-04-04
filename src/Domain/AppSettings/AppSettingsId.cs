using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AppSettings;

public readonly record struct AppSettingsId
{
    public Guid Value { get; init; }

    public static implicit operator Guid(AppSettingsId appSettingsId) => appSettingsId.Value;
    public static implicit operator AppSettingsId(Guid value) => new AppSettingsId(value);
    public AppSettingsId(Guid value)
    {
        if (value.Version != 7 || value == default)
            throw new ArgumentException("AppSettingsId is invalid.", nameof(value));

        Value = value;
    }
}
