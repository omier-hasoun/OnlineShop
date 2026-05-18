using Domain.ProductsGroups.ValueObjects;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.ProductsGroups.Products;

public sealed class Product : BaseEntity<ProductId>
{
    private Product()
    {
    }

    private Product(ProductId id, ProductGroupId productsGroupId, Money? priceBeforeDiscount, Money price, byte? discountPercentage, DateOnly? discountExpiresOn, ProductStatus status,
        int width, int height, int length, int weight, string sku, string slug, string barCode, Dictionary<string, string> specifications, List<ProductImage> images)
        : base(id)
    {
        ProductsGroupId = productsGroupId;
                
        PriceAfterDiscount = priceBeforeDiscount;
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

    public static Result<Product> Create(ProductId id, ProductGroupId productsGroupId, Money Price, int width, int height, int length,
        int weight, string sku, string slug, string barCode, Dictionary<string, string> specifications, List<ProductImage>? images = null)
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
        images  ??= [];


        return new Product(id, productsGroupId, priceBeforeDisount, Price, discountPercentage, discountExpiresOn, status,
            width, height, length, weight, sku, slug, barCode, specifications, images);
    }

    public ProductGroupId ProductsGroupId { get; private init; }

    public bool HasActiveDiscount =>
        DiscountPercentage is not null &&
        DiscountExpiresOn.HasValue &&
        ValHelper.IsDateInFuture(DiscountExpiresOn);

    public int Width { get; private set; }
    public int Height { get; private set; }
    public int Length { get; private set; }
    public int Weight { get; private set; }

    public Money Price { get; private set; } = null!;

    public DateOnly? DiscountExpiresOn { get; private set; }

    public Money? PriceAfterDiscount { get; private set; }
    public byte? DiscountPercentage { get; private set;}

    public ProductStatus Status { get; private set; }

    public string Sku { get; private set; } = null!;

    public string Slug { get; private set; } = null!;

    public string BarCode { get; private init; } = null!;


    private List<ProductImage> _images = [];
    public IReadOnlyCollection<ProductImage> Images { get { return _images.AsReadOnly(); } private set { _images = value is null ? [] : value.ToList(); } }


    private Dictionary<string, string> _specifications = [];
    public IReadOnlyDictionary<string, string> Specifications { get { return _specifications.AsReadOnly(); } private set { _specifications = value is null ?[] :value.ToDictionary(); } }

    public Result<Updated> AddImages(List<string> fileNames)
    {
        if (fileNames is null || fileNames.Count == 0)
            return DomainErrors.MissingInput;

        int totalImagesCount = fileNames.Count + _images.Count;

        if (totalImagesCount > ProductRules.MaxNumberOfImages)
            return DomainErrors.Products.ImagesLimitExceeded.WithParameters(ProductRules.MaxNumberOfImages);

        byte sortOrder = (byte)(_images.Count + 1);

        for (int i = 0; sortOrder <= totalImagesCount; i++)
        {
            _images.Add(ProductImage.Create(fileNames[i], sortOrder++));
        }

        return Result.Updated;
    }

    public Result<Deleted> RemoveImages(List<string> fileNames)
    {
        if (fileNames is null || fileNames.Count == 0)
            return DomainErrors.MissingInput;

        if (fileNames.Count > _images.Count)
            return DomainErrors.Products.ImagesCountMustMatchProductImagesCount;

        foreach (var name in fileNames)
        {
            if (!_images.Exists(x => x.FileName == name))
            {
                return DomainErrors.Products.InvalidImageFileName.WithParameters(name);
            }
            
        }
        foreach(var name in fileNames)
        {
            var image = _images.FirstOrDefault(x => x.FileName == name);

            // happens if the list contains duplicate filenames which should not happen but if it happend we just skip it
            if (image is null)
                continue;

            _images.Remove(image);
        }
        SortImages();
        return Result.Deleted;
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

    private void SortImages()
    {
        byte sortOrder = 1;
        List<ProductImage> sortedImages = new(_images.Count);

        foreach (var image in _images.OrderBy(i => i.SortOrder))
        {
            sortedImages.Add(image.ChangeSortOrder(sortOrder++));
        }
        _images = sortedImages;
        
    }

    public Result<Success> UpdateImagesSortOrder(IReadOnlyCollection<ProductImage> images)
    {
        if (images is null || images.Count != _images.Count)
        {
            return DomainErrors.Products.ImagesCountMustMatchProductImagesCount;
        }

        foreach (var image in images)
        {
            // no need to check strictly here this is enough
            if (!_images.Any(x => x.FileName == image.FileName))
            {
                return DomainErrors.Products.ImagesNamesMustMatchProductImagesNames;
            }

        }

        Images = images;
        SortImages();
        return Result.Success;
    }

    public Result<Success> ApplyDiscount(DateOnly discountExpiresOn, byte discountPercentage)
    {
        if (Status == ProductStatus.Archived)
            return DomainErrors.Products.ThisProductIsArchivedAndCannotBeModified;

        if (Price.Value < ProductRules.MinPriceToApplyADiscount)
        {
            return DomainErrors.Products.ProductPriceNotApplicableForDiscount;
        }

        var valResult = ValidateDiscountPercentage(discountPercentage);

        if (valResult.Failed)
            return valResult;

        var val2Result = ValidateDiscountExpiresOn(discountExpiresOn);

        if (val2Result.Failed)
            return val2Result;

        PriceAfterDiscount = Money.From(Price.Value * (100 - discountPercentage) / 100).Value;
        this.DiscountExpiresOn = discountExpiresOn;
        this.DiscountPercentage = discountPercentage;
        return Result.Success;
    }


    #region validators
    private static Result<Success> ValidatePrice(Money price)
    {
        if(ValHelper.IsOutOfRange(price.Value, ProductRules.MinPrice, ProductRules.MaxPrice))
        {
            return DomainErrors.Products.PriceOutOfRange;
        }
        return Result.Success;
    }

    private static Result<Success> ValidateDiscountPercentage(byte discountPercentage)
    {
        if (ValHelper.IsOutOfRange(discountPercentage, ProductRules.MinDiscountPercentageValue, ProductRules.MaxDiscountPercentageValue))
        {
            return DomainErrors.Products.DiscountValueOutOfRange;
        }
        return Result.Success;
    }

    private static Result<Success> ValidateDiscountExpiresOn(DateOnly discountExpiresOn)
    {
        if (!ValHelper.IsDateInFuture(discountExpiresOn))
        {
            return DomainErrors.Products.DateMustBeInFuture;
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
            return DomainErrors.Products.ImagesNamesIsEmpty;
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
    #endregion
}
