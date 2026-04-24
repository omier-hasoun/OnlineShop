namespace Domain.Common.EntitiesRules;

public static class ProductRules
{
    // Name length limits
    public const byte MinTitleLength = 5;
    public const byte MaxTitleLength = 100;

    // Description length limits
    public const byte MinDescriptionLength = 20;
    public const short MaxDescriptionLength = 300;

    // Product images count limits
    public const byte MinProductImagesCount = 1;
    public const byte MaxProductImagesCount = 50;

    // Rating limits. 1 to 5 stars or 0 if no one rated it 
    public const byte MinAverageRatingValue = 0;
    public const byte MaxAverageRatingValue = 5;

    public const short MaxValueOf_MaxQuantityPerCustomer = 1000;
    public const byte MinValueOf_MaxQuantityPerCustomer = 1;

}
