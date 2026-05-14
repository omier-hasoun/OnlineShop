using Domain.ProductsGroups.ValueObjects;

namespace Domain.ProductsGroups.Products;

public sealed class Product : BaseEntity<ProductId>
{
    private Product()
    {
    }

    private Product(ProductId id, ProductsGroupId productsGroupId, Money? priceBeforeDiscount, Money price, byte? discountPercentage, DateOnly? discountExpiresOn, ProductStatus status,
        int width, int height, int length, int weight, string sku, string slug, string barCode, Dictionary<string, string> specifications, List<ProductImage> images)
        : base(id)
    {
        ProductsGroupId = productsGroupId;
                
        PriceBeforeDiscount = priceBeforeDiscount;
        DiscountPercentage = discountPercentage;
        DiscountExpiresOn = discountExpiresOn;
        Price = price;
        Status = status;
        Width = width;
        Height = height;
        Length = length;
        Weight = weight;

        Sku = sku;
        Slug = slug;
        BarCode = barCode;
        _images = images;
        _specifications = specifications;
    }

    public static Result<Product> Create(ProductId id, ProductsGroupId productsGroupId, Money Price, int width, int height, int length,
        int weight, string sku, string slug, string barCode, Dictionary<string, string> specifications)
    {
        var validationResult = Result.ValidateAll(
                                () => id.IsValid(),
                                () => ValidatePrice(Price),
                                () => Validate_Width_Height_Length(width, height, length),
                                () => ValidateSpecifications(specifications),
                                () => ValidateSku(sku),
                                () => ValidateSlug(slug),
                                () => ValidateBarcode(barCode));

        if (validationResult.Failed)
        {
            return validationResult.Errors;
        }

        //defaults

        byte? discountPercentage = null;
        Money? priceBeforeDisount = null;
        DateOnly? discountExpiresOn = null;
        ProductStatus status = ProductStatus.Draft;
        List<ProductImage> images = [];


        return new Product(id, productsGroupId, priceBeforeDisount, Price, discountPercentage, discountExpiresOn, status,
            width, height, length, weight, sku, slug, barCode, specifications, images);
    }

    public ProductsGroupId ProductsGroupId { get; private init; }

    public bool HasActiveDiscount =>
        DiscountPercentage is not null &&
        DiscountExpiresOn.HasValue &&
        DateOnly.FromDateTime(DateTime.UtcNow) <= DiscountExpiresOn.Value;

    public int Width { get; private set; }
    public int Height { get; private set; }
    public int Length { get; private set; }
    public int Weight { get; private set; }

    public Money Price { get; private set; } = null!;

    public DateOnly? DiscountExpiresOn { get; private set; }

    public Money? PriceBeforeDiscount { get; private set; }
    public byte? DiscountPercentage { get; private set;}

    public ProductStatus Status { get; private set; }

    public string Sku { get; private set; } = null!;

    public string Slug { get; private set; } = null!;

    public string BarCode { get; private init; } = null!;


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
        Status = ProductStatus.Published;
    }
    public void Unpublish()
    {
        Status = ProductStatus.Unpublished;
    }
    public void Archive()
    {
        Status = ProductStatus.Archived;
    }

    public Result<Success> ApplyDiscount()
    {
        throw new NotImplementedException();
    }


    private void EnsureImagesHaveSequentialSortOrder()
    {
        byte sortOrder = 1;
        foreach (var image in Images.OrderBy(i => i.SortOrder))
        {
            image.ChangeSortOrder(sortOrder++);
        }
    }

    private static Result<Success> ValidateImages(List<ProductImage> newImages)
    {
        if (newImages is null || ValHelper.IsOutOfRange(newImages.Count, ProductRules.MinNumberOfImages, ProductRules.MaxNumberOfImages))
        {
            return DomainErrors.Products.ImagesOutOfRange;
        }
        return Result.Success;
    }
    private static Result<Success> ValidatePrice(Money price)
    {
        if(ValHelper.IsOutOfRange(price.Value, ProductRules.MinPrice, ProductRules.MaxPrice))
        {
            return DomainErrors.Products.PriceOutOfRange;
        }
        return Result.Success;
    }

    private static Result<Success> Validate_Width_Height_Length(int width, int height, int length)
    {
        if
        (
            ValHelper.IsOutOfRange(width, ProductRules.Min_Height_Width_Length_cm, ProductRules.Max_Height_Width_Length_cm) ||
            ValHelper.IsOutOfRange(height, ProductRules.Min_Height_Width_Length_cm, ProductRules.Max_Height_Width_Length_cm) ||
            ValHelper.IsOutOfRange(length, ProductRules.Min_Height_Width_Length_cm, ProductRules.Max_Height_Width_Length_cm)
        )
        {
            return DomainErrors.Products.InvalidDimensions;
        }
        return Result.Success;
    }

    private static Result<Success> ValidateBarcode(string barcode)
    {
        if(string.IsNullOrEmpty(barcode))
        {
            return DomainErrors.Products.BarCodeRequired;
        }

        if(ValHelper.IsOutOfRange(barcode.Length, ProductRules.MinBarcodeLength, ProductRules.MaxBarcodeLength))
        {
            return DomainErrors.Products.BarCodeOutOfRange;
        }
        return Result.Success;
    }
    private static Result<Success> ValidateSlug(string slug)
    {

        if (string.IsNullOrEmpty(slug) || ValHelper.IsOutOfRange(slug.Length, ProductRules.MinSlugLength, ProductRules.MaxSlugLength))
        {
            return DomainErrors.Products.SlugLengthOutOfRange;
        }

        return Result.Success;
    }

    private static Result<Success> ValidateSku(string sku)
    {

        if (string.IsNullOrEmpty(sku) || ValHelper.IsOutOfRange(sku.Length, ProductRules.MinSkuLength, ProductRules.MaxSkuLength))
        {
            return DomainErrors.Products.SkuOutOfRange;
        }

        return Result.Success;
    }

    private static Result<Success> ValidateSpecifications(Dictionary<string,string> specifications)
    {
        if(specifications is null || specifications.Count == 0)
        {
            return DomainErrors.Products.AtleastOneSpecificationRequired;
        }

        if (specifications.Count > ProductRules.MaxNumberOfSpecifications)
        {
            return DomainErrors.Products.MaxAllowedSpecificationsNumberExceeded;
        }

        foreach ( var spec in specifications)
        {
            bool invalidKey =
            string.IsNullOrEmpty(spec.Key) || ValHelper.IsOutOfRange(spec.Key.Length, ProductRules.MinSpecificationKeyLength, ProductRules.MaxSpecificationKeyLength);

            bool invalidValue = string.IsNullOrEmpty(spec.Value) ||
            ValHelper.IsOutOfRange(spec.Value.Length, ProductRules.MinSpecificationValueLength, ProductRules.MaxSpecificationValueLength);


            if (invalidKey & invalidValue)
            {
                return DomainErrors.Products.InvalidSpecification;
            }
        }

        return Result.Success;
    }
}
