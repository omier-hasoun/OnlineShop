namespace Domain.Common.Entities.Addresses;

public sealed record GeoLocation
{
    internal GeoLocation()
    {
        
    }
    public GeoLocation(decimal latitude, decimal longitude)
    {
        if(latitude < -90 || latitude > 90)
        {
            throw new ArgumentOutOfRangeException(nameof(latitude), "Latitude must be between -90 and 90.");
        }
        if(longitude < -180 || longitude > 180)
        {
            throw new ArgumentOutOfRangeException(nameof(longitude), "Longitude must be between -180 and 180.");
        }
        Latitude = latitude;
        Longitude = longitude;
    }

    public decimal Latitude { get; }
    public decimal Longitude { get; }
}
