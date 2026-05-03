
using Domain.Common.ValueObjects;
using Domain.Products.ValueObjects;

namespace Domain.Products.ProductVariants;

public sealed class ProductVariant : BaseEntity<ProductVariantId>
{
    private ProductVariant()
    {
    }
    private ProductVariant(ProductVariantId id, ProductId productId, Money originalPrice, Money priceNow, byte discountPercentage,  ProductStatus status,
        int width, int height, int length, int weight, string sku, string slug, string barCode, IReadOnlyDictionary<string, string> specifications, IReadOnlyCollection<ProductImage>? images)
        : base(id)
    {
        ProductId = productId;
                
        OriginalPrice = originalPrice;
        DiscountPercentage = discountPercentage;
        PriceNow = priceNow;
        Status = status;
        Width = width;
        Height = height;
        Length = length;
        Weight = weight;

        Sku = sku;
        Slug = slug;
        Barcode = barCode;

        _specifications = specifications.ToDictionary();
    }

    public static Result<ProductVariant> Create(ProductVariantId id, ProductId productId, Money originalPrice,
        int width, int height, int length, int weight, string sku, string slug, string barCode, IReadOnlyDictionary<string, string> specifications, IReadOnlyCollection<ProductImage>? images = null)
    {
        var validationResult = Result.ValidateAll(
                                () => id.Validate(),
                                () => ValidateOriginalPrice(originalPrice),
                                () => Validate_Width_Height_Length(width, height, length),
                                () => ValidateSpecifications(specifications),
                                () => ValidateSku(sku),
                                () => ValidateSlug(slug),
                                () => ValidateBarcode(barCode)
                               );

        if (validationResult.Failed)
        {
            return validationResult.Errors;
        }

        //defaults

        byte discountPercentage = 0;
        Money priceNow = originalPrice;
        ProductStatus status = ProductStatus.NotActive;
        
        return new ProductVariant(id, productId, originalPrice, priceNow, discountPercentage, status,
            width, height, length, weight, sku, slug, barCode, specifications, images);
    }

    public ProductId ProductId { get; private set; }

    public int Width { get; private set; }
    public int Height { get; private set; }
    public int Length { get; private set; }
    public int Weight { get; private set; }

    /// <summary>
    /// Product's original Price, used only to show the orginal price before discount
    /// </summary>
    public Money OriginalPrice { get; } = null!;

    /// <summary>
    /// Product's price now, used in checkout
    /// </summary>
    public Money PriceNow { get; } = null!;
    public ProductStatus Status { get; private set; }
    public byte DiscountPercentage { get; private set;}

    public string Sku { get; private set; } = null!;
    public string Slug { get; private set; } = null!;
    public string Barcode { get; private init; }

    private List<ProductImage> _images = [];
    public IReadOnlyCollection<ProductImage> Images { get { return _images.AsReadOnly(); } private set { _images = value is null ? [] : value.ToList(); } }

    private Dictionary<string, string> _specifications = [];
    public IReadOnlyDictionary<string, string> Specifications { get { return _specifications.AsReadOnly(); } private set { _specifications = value is null ?[] :value.ToDictionary(); } }

    //private static decimal CalculateDiscountPrice(decimal originalPrice, byte discountPercentage = 0)
    //{
    //    if (discountPercentage == 0)
    //        return originalPrice;
    //    return originalPrice * (100 - discountPercentage) / 100;
    //}
    private void EnsureImagesSortOrderIsSequential(ref List<ProductImage> images)
    {
        byte sortOrder = 1;
        foreach (var image in images.OrderBy(i => i.SortOrder))
        {
            image.ChangeSortOrder(sortOrder++);
        }
    }

    public Result<Updated> UpdateImages(List<ProductImage> newImages)
    {
        
        if(newImages is null || ValHelper.IsOutOfRange(newImages.Count, ProductVariantRules.MinNumberOfImages, ProductVariantRules.MaxNumberOfImages))
        {
            return DomainErrors.ProductVariants.ImagesOutOfRange;
        }
        EnsureImagesSortOrderIsSequential(ref newImages);
        _images = newImages;
        return Result.Updated;
    }

    private static Result<Success> ValidateOriginalPrice(Money price)
    {
        if(ValHelper.IsOutOfRange(price.Value, ProductVariantRules.MinOriginalPriceValue, ProductVariantRules.MaxOriginalPriceValue))
        {
            return DomainErrors.ProductVariants.PriceOutOfRange;
        }
        return Result.Success;
    }

    private static Result<Success> Validate_Width_Height_Length(int width, int height, int length)
    {
        if
        (
            ValHelper.IsOutOfRange(width, ProductVariantRules.Min_Height_Width_Length_cm, ProductVariantRules.Max_Height_Width_Length_cm) ||
            ValHelper.IsOutOfRange(height, ProductVariantRules.Min_Height_Width_Length_cm, ProductVariantRules.Max_Height_Width_Length_cm) ||
            ValHelper.IsOutOfRange(length, ProductVariantRules.Min_Height_Width_Length_cm, ProductVariantRules.Max_Height_Width_Length_cm)
        )
        {
            return DomainErrors.ProductVariants.InvalidDimensions;
        }
        return Result.Success;
    }

    private static Result<Success> ValidateBarcode(string barcode)
    {
        if(string.IsNullOrEmpty(barcode))
        {
            return DomainErrors.ProductVariants.BarCodeRequired;
        }

        if(ValHelper.IsOutOfRange(barcode.Length, ProductVariantRules.MinBarcodeLength, ProductVariantRules.MaxBarcodeLength))
        {
            return DomainErrors.ProductVariants.BarcodeOutOfRange;
        }
        return Result.Success;
    }
    private static Result<Success> ValidateSlug(string slug)
    {

        if (string.IsNullOrEmpty(slug) || ValHelper.IsOutOfRange(slug.Length, ProductVariantRules.MinSlugLength, ProductVariantRules.MaxSlugLength))
        {
            return DomainErrors.ProductVariants.SlugLengthOutOfRange;
        }

        return Result.Success;
    }

    private static Result<Success> ValidateSku(string sku)
    {

        if (string.IsNullOrEmpty(sku) || ValHelper.IsOutOfRange(sku.Length, ProductVariantRules.MinSkuLength, ProductVariantRules.MaxSkuLength))
        {
            return DomainErrors.ProductVariants.SkuOutOfRange;
        }

        return Result.Success;
    }

    private static Result<Success> ValidateSpecifications(IReadOnlyDictionary<string,string> specifications)
    {
        if(specifications is null || specifications.Count == 0)
        {
            return DomainErrors.ProductVariants.AtleastOneSpecificationRequired;
        }

        if (specifications.Count > ProductVariantRules.MaxNumberOfSpecifications)
        {
            return DomainErrors.ProductVariants.MaxAllowedSpecificationsNumberExceeded;
        }

        foreach ( var spec in specifications)
        {
            bool validSpecificationKey =
            string.IsNullOrEmpty(spec.Key) || ValHelper.IsOutOfRange(spec.Key.Length, ProductVariantRules.MinSpecificationKeyLength, ProductVariantRules.MaxSpecificationKeyLength);

            bool validSpecificationValue = string.IsNullOrEmpty(spec.Value) ||
            ValHelper.IsOutOfRange(spec.Value.Length, ProductVariantRules.MinSpecificationValueLength, ProductVariantRules.MaxSpecificationValueLength);


            if (validSpecificationKey & validSpecificationValue)
            {
                return DomainErrors.ProductVariants.InvalidSpecification;
            }
        }

        return Result.Success;
    }
}
