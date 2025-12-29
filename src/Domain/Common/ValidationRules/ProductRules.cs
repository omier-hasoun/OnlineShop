namespace Domain.Common.ValidationRules;

public static class ProductRules
{
    public const int NameMaxLength = 64;
    public const int NameMinLength = 3;
    public const int DescriptionMinLength = 32;
    public const int DescriptionMaxLength = 256;
    public const int MadeByCompanyMinLength = 1;
    public const int MadeByCompanyMaxLength = 32;
    public const int PriceMinValue = 5;
    public const int PriceMaxValue = 10_000_000;

    public static class ReviewRules
    {
        public const int CommentMinLength = 10;
        public const int CommentMaxLength = 128;
        public const int RatingMaxValue = 5;
        public const int RatingMinValue = 1;
    }

}
