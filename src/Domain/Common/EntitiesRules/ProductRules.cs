namespace Domain.Common.EntitiesRules;

public static class ProductRules
{
    // Name length limits
    public const byte MinTitleLength = 3;
    public const byte MaxTitleLength = 64;

    // Description length limits
    public const byte MinDescriptionLength = 32;
    public const short MaxDescriptionLength = 256;

    // Manufacturer / "MadeByCompany" length limits
    public const byte MinBrandLength = 1;
    public const byte MaxBrandLength = 64;


    // Product images count limits
    public const byte MinProductImagesCount = 1;
    public const byte MaxProductImagesCount = 30;

    // Rating limits
    public const byte MinRatingValue = 1;
    public const byte MaxRatingValue = 5;


}
