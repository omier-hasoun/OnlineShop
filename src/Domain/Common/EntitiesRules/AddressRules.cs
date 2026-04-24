
namespace Domain.Common.EntitiesRules;

public static class AddressRules
{
    public const byte MaxPostalCodeLength = 15;
    public const byte MinPostalCodeLength = 4;

    public const byte MaxAddressLine1Length = 255;
    public const byte MaxAddressLine2Length = 100;
    public const byte MaxPhoneNumberLength = 25;
    public const byte MaxCityLength = 200;
    public const byte MaxHouseNoLength = 10;
    public const byte MaxCountryCodeLength = 2;
    public const byte MaxStateProvinceLength = 100;
    public const byte MaxNotesLength = 100;

    public const byte MaxFullNameLength = 100;
    public const byte MinFullNameLength = 4;

}
