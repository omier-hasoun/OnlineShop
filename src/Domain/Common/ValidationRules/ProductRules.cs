namespace Domain.Common.ValidationRules;

public static partial class ProductRules
{
    // Name length limits
    public const byte MinNameLength = 3;
    public const byte MaxNameLength = 64;

    // Description length limits
    public const byte MinDescriptionLength = 32;
    public const short MaxDescriptionLength = 256;

    // Manufacturer / "MadeByCompany" length limits
    public const byte MinManufacturerLength = 1;
    public const byte MaxManufacturerLength = 64;

    // Price limits
    public const byte MinDefaultPriceValue = 5;
    public const int MaxDefaultPriceValue = 10_000_000;

    // Quantity limits
    public const byte MinQuantityValue = 0;
    public static readonly short MaxQuantityValue = short.MaxValue;

    // Product images count limits
    public const byte MinProductImagesCount = 1;
    public const byte MaxProductImagesCount = 30;

    // Rating limits
    public const byte MinRatingValue = 1;
    public const byte MaxRatingValue = 5;


}
