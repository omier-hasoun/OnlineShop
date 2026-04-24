
using Domain.Common.ValueObjects;

namespace Domain.Products;

public sealed class Product : AggregateRoot<ProductId>, IFullAudited, ISoftDeleted
{
    private Product() : base()
    {
    }
    private Product(ProductId id, BrandId brandId, CategoryId categoryId, string title, string description, float averageRating, Money defaultOriginalPrice,
        Money defaultDiscountPrice, byte defaultDiscountPercentage, bool containsAlcohol, bool isDangerousGood, bool isBiological,
        bool isSerialized, bool isDeleted, short maxQuantityPerCustomer, IReadOnlyList<string> attributes,
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
        IsSerialized = isSerialized;
        MaxQuantityPerCustomer = maxQuantityPerCustomer;
        Attributes = attributes;
        IsDeleted = isDeleted;  
        AverageRating = averageRating;
        DefaultOriginalPrice = defaultOriginalPrice;
        DefaultDiscountPercentage = defaultDiscountPercentage;
        DefaultDiscountPrice = defaultDiscountPrice;

        CreatedAt = createdAt;
        LastModifiedAt = lastModifiedAt;
        CreatedBy = createdBy;
        LastModifiedBy = lastModifiedBy;
    }
    public static Result<Product> Create(ProductId id, BrandId brandId, CategoryId categoryId, string title, string description, Money defaultOriginalPrice, bool containsAlcohol, bool isDangerousGood,
        bool isBiological, bool isSerialized, short maxQuantityPerCustomer, IReadOnlyList<string>? attributes)
    {
        // Add domain validation logic here

        //defaults initialization
        float averageRating = 0;
        bool isDeleted = false;
        Money defaultDiscountPrice = defaultOriginalPrice;
        byte defaultDiscountPercentage = 0;
        DateTime createdAt = TimeService.UtcNow;
        DateTime lastModifiedAt = createdAt;
        UserId lastModifiedBy = UserId.EmptyInstance;
        UserId createdBy = lastModifiedBy;

        attributes ??= [];

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
            isSerialized: isSerialized,
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
    public short MaxQuantityPerCustomer { get; private set; }
    public Money DefaultOriginalPrice { get; }
    public Money DefaultDiscountPrice { get;}
    public byte DefaultDiscountPercentage { get; private set; }

    public bool ContainsAlcohol { get; private set; }
    public bool IsDangerousGood { get; private set; }
    public bool IsBiological { get; private set; }
    public bool IsSerialized { get; private set; }


    public bool IsDeleted { get; private set; }
    public UserId CreatedBy { get; set; }
    public UserId LastModifiedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }


    List<string> _attributes = [];
    public IReadOnlyList<string> Attributes { get { return _attributes.AsReadOnly(); } private set { _attributes = value is null ? [] :value.ToList(); } }

    private List<ProductImage> _images = [];
    public IReadOnlyCollection<ProductImage> Images { get { return _images.AsReadOnly(); } private set { _images = value is null ? [] : value.ToList(); } }


    private List<ProductVariant> _variants = [];
    public IReadOnlyCollection<ProductVariant> Variants { get { return _variants.AsReadOnly(); } private set { _variants = value is null ? [] : value.ToList(); } }



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
