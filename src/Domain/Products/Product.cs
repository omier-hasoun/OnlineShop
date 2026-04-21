
namespace Domain.Products;

public sealed class Product : AggregateRoot<ProductId>, IFullAudited, ISoftDeleted
{
    private Product(ProductId id, BrandId brandId, CategoryId categoryId, string title, string description, float averageRating, decimal defaultOriginalPrice,
        decimal defaultDiscountPrice, byte defaultDiscountPercentage, bool containsAlcohol, bool isDangerousGood, bool isBiological,
        bool requiresSN, bool isDeleted, byte maxQuantityPerCustomer, IReadOnlyDictionary<string, string>? attributes,
        DateTime createdAt, DateTime lastModifiedAt, UserId createdBy, UserId lastModifiedBy)
            : base(id)
    {
        BrandId = brandId;
        CategoryId = categoryId;
        Title = title;
        Description = description;
        ContainsAlcohol = containsAlcohol;
        IsDangerousGood = isDangerousGood;
        IsBiological = isBiological;
        IsSerialized = requiresSN;
        MaxQuantityPerCustomer = maxQuantityPerCustomer;
        Attributes = attributes;
        IsDeleted = isDeleted;  
        AverageRating = averageRating;
        DefaultOriginalPrice = defaultOriginalPrice;
        DefaultDiscountPercentage = defaultDiscountPercentage;
        DefaultCurrentPrice = defaultDiscountPrice;

        CreatedAt = createdAt;
        LastModifiedAt = lastModifiedAt;
        CreatedBy = createdBy;
        LastModifiedBy = lastModifiedBy;
    }
    public static Result<Product> Create(ProductId id, BrandId brandId, CategoryId categoryId, string title, string description, decimal defaultOriginalPrice, bool containsAlcohol, bool isDangerousGood,
        bool isBiological, bool isSerialized, byte maxQuantityPerCustomer, IReadOnlyDictionary<string, string>? attributes)
    {
        // Add domain validation logic here

        //defaults initialization
        float averageRating = 0;
        bool isDeleted = false;
        decimal defaultDiscountPrice = defaultOriginalPrice;
        byte defaultDiscountPercentage = 0;
        DateTime createdAt = TimeService.UtcNow;
        DateTime lastModifiedAt = createdAt;
        UserId lastModifiedBy = UserId.EmptyInstance;
        UserId createdBy = lastModifiedBy;

        var product = new Product(
            id : id,
            brandId : brandId,
            categoryId: categoryId,
            title: title,
            description: description,
            averageRating: averageRating,
            defaultOriginalPrice : defaultOriginalPrice,
            defaultDiscountPrice: defaultDiscountPrice,
            defaultDiscountPercentage: defaultDiscountPercentage,
            containsAlcohol : containsAlcohol,
            isDangerousGood: isDangerousGood,
            isBiological: isBiological,
            requiresSN: isSerialized,
            isDeleted: isDeleted,
            maxQuantityPerCustomer: maxQuantityPerCustomer,
            attributes: attributes,
            createdAt: createdAt,
            lastModifiedAt: lastModifiedAt,
            createdBy: createdBy,
            lastModifiedBy: lastModifiedBy
            );

        return product;
    }


    public CategoryId CategoryId { get; private init; }
    public BrandId BrandId { get; private init; }

    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public float AverageRating { get; private set; }
    public byte MaxQuantityPerCustomer { get; private set; }
    public decimal DefaultOriginalPrice { get; private set; }
    public decimal DefaultCurrentPrice { get; private set; }
    public byte DefaultDiscountPercentage { get; private set; }

    public bool ContainsAlcohol { get; private set; }
    public bool IsDangerousGood { get; private set; }
    public bool IsBiological { get; private set; }
    public bool IsSerialized { get; private set; }

    Dictionary<string, string>? _attributes = null;
    public IReadOnlyDictionary<string, string>? Attributes { get { return _attributes is null ? null : _attributes.AsReadOnly(); } private set { _attributes = value?.ToDictionary(); } }

    public bool IsDeleted { get; private set; }
    public UserId CreatedBy { get; set; }
    public UserId LastModifiedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }

    private List<ProductReview>? _reviews;
    public IReadOnlyCollection<ProductReview>? Reviews { get { return _reviews?.AsReadOnly(); } private set { _reviews = value?.ToList(); } }


    private List<ProductImage>? _images;
    public IReadOnlyCollection<ProductImage>? Images { get { return _images?.AsReadOnly(); } private set { _images = value?.ToList(); } }
    private List<ProductVariant>? _variants;
    public IReadOnlyCollection<ProductVariant>? Variants { get { return _variants?.AsReadOnly(); } private set { _variants = value?.ToList(); } }

    public Brand BrandInfo { get; private set; } = null!;


    public Result<Updated> UpdateProductImages(List<ProductImage> newImages)
    {
        if (newImages.Count < ProductRules.MinProductImagesCount || newImages.Count > ProductRules.MaxProductImagesCount)
        {
            return ProductErrors.ImagesOutOfRange;
        }
        EnsureImagesSortOrderIsSequential(ref newImages);
        _images = newImages;
        return Result.Updated;
    }
    private static void EnsureImagesSortOrderIsSequential(ref List<ProductImage> images)
    {
        byte sortOrder = 1;
        foreach (var image in images.OrderBy(i => i.SortOrder))
        {
            image.UpdateSortOrder(sortOrder++);
        }
    }
}
