using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AppSettings;

public sealed class AppSettings : BaseEntity, IFullAudited
{
    private AppSettings()
    {
    }

    public static Result<AppSettings> Create(AppSettingsId id, string key, string value)
    {
            return new AppSettings
            {
                Id = id,
                Key = key,
                Value = value,
            };
    }
    public AppSettingsId Id { get; private set; }
    public string Key { get; private init; } = null!;
    public string Value { get; private set; } = null!;

    public UserId CreatedBy { get; set; }
    public UserId LastModifiedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }

}
