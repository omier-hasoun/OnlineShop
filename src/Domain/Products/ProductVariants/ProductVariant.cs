
using Domain.Common.ValueObjects;
using Domain.Products.ValueObjects;

namespace Domain.Products.ProductVariants;

public sealed class ProductVariant : BaseEntity<ProductVariantId>
{
    private ProductVariant()
    {
    }

    private ProductVariant(ProductVariantId id, ProductId productId, Money? priceBeforeDiscount, Money price, byte discountPercentage,  ProductStatus status,
        int width, int height, int length, int weight, string sku, string slug, string barCode, IReadOnlyDictionary<string, string> specifications, IReadOnlyCollection<ProductImage> images)
        : base(id)
    {
        ProductId = productId;
                
        PriceBeforeDiscount = priceBeforeDiscount;
        DiscountPercentage = discountPercentage;
        Price = price;
        Status = status;
        Width = width;
        Height = height;
        Length = length;
        Weight = weight;

        Sku = sku;
        Slug = slug;
        Barcode = barCode;
        _images = images.ToList();
        _specifications = specifications.ToDictionary();
    }

    public static Result<ProductVariant> Create(ProductVariantId id, ProductId productId, Money Price,
        int width, int height, int length, int weight, string sku, string slug, string barCode, IReadOnlyDictionary<string, string> specifications, IReadOnlyCollection<ProductImage> images)
    {
        var validationResult = Result.ValidateAll(
                                () => id.Validate(),
                                () => ValidatePrice(Price),
                                () => Validate_Width_Height_Length(width, height, length),
                                () => ValidateSpecifications(specifications),
                                () => ValidateSku(sku),
                                () => ValidateSlug(slug),
                                () => ValidateImages(images),
                                () => ValidateBarcode(barCode));

        if (validationResult.Failed)
        {
            return validationResult.Errors;
        }

        //defaults

        byte discountPercentage = 0;
        Money? priceBeforeDisount = null;
        ProductStatus status = ProductStatus.Draft;
        
        return new ProductVariant(id, productId, priceBeforeDisount, Price, discountPercentage, status,
            width, height, length, weight, sku, slug, barCode, specifications, images);
    }

    public ProductId ProductId { get; private init; }

    public int Width { get; private set; }
    public int Height { get; private set; }
    public int Length { get; private set; }
    public int Weight { get; private set; }

    public Money Price { get; private init; } = null!;
    public Money? PriceBeforeDiscount { get; private init; }
    public byte DiscountPercentage { get; private set;}

    public ProductStatus Status { get; private set; }

    public string Sku { get; private set; } = null!;
    public string Slug { get; private set; } = null!;
    public string Barcode { get; private init; } = null!;

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


    public Result<Updated> UpdateImages(List<ProductImage> newImages)
    {
        var validaitonResult = ValidateImages(newImages);

        if (validaitonResult.Failed)
            return validaitonResult.Errors;

        _images = newImages;
        EnsureImagesHaveSequentialSortOrder();

        return Result.Updated;
    }


    public void Publish()
    {
        Status = ProductStatus.Active;
    }
    public void Unpublish()
    {
        Status = ProductStatus.NotActive;
    }
    public void MarkDeleted()
    {
        Status = ProductStatus.Archived;
    }


    private void EnsureImagesHaveSequentialSortOrder()
    {
        byte sortOrder = 1;
        foreach (var image in Images.OrderBy(i => i.SortOrder))
        {
            image.ChangeSortOrder(sortOrder++);
        }
    }





    private static Result<Success> ValidateImages(IReadOnlyCollection<ProductImage> newImages)
    {
        if (newImages is null || ValHelper.IsOutOfRange(newImages.Count, ProductVariantRules.MinNumberOfImages, ProductVariantRules.MaxNumberOfImages))
        {
            return DomainErrors.ProductVariants.ImagesOutOfRange;
        }
        return Result.Success;
    }
    private static Result<Success> ValidatePrice(Money price)
    {
        if(ValHelper.IsOutOfRange(price.Value, ProductVariantRules.MinPrice, ProductVariantRules.MaxPrice))
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
            bool invalidKey =
            string.IsNullOrEmpty(spec.Key) || ValHelper.IsOutOfRange(spec.Key.Length, ProductVariantRules.MinSpecificationKeyLength, ProductVariantRules.MaxSpecificationKeyLength);

            bool invalidValue = string.IsNullOrEmpty(spec.Value) ||
            ValHelper.IsOutOfRange(spec.Value.Length, ProductVariantRules.MinSpecificationValueLength, ProductVariantRules.MaxSpecificationValueLength);


            if (invalidKey & invalidValue)
            {
                return DomainErrors.ProductVariants.InvalidSpecification;
            }
        }

        return Result.Success;
    }
}
