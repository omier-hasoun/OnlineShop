
namespace Domain.AppSettings;

public sealed class AppSettings : AggregateRoot<AppSettingsId>, IHasModificationTime
{
    //private AppSettings() : base()
    //{
        
    //}

    private AppSettings(AppSettingsId id, string value, string description, DateTime lastModifiedAt) : base(id)
    {
        Description = description;
        Value = value;
        LastModifiedAt = lastModifiedAt;
    }


    // no creation feature, can only be created in db because it needs to integrate with the code

    //public static Result<AppSettings> Create(AppSettingsId key, string value, string? description)
    //{

    //    return new AppSettings(key, value, description, TimeService.UtcNow);
    //}


    public AppSettingsId Key { get { return Id; } } // derrived property for clearity 
    public string Value { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public DateTime LastModifiedAt { get; set; }

}
