
using Domain.Products.ValueObjects;

namespace Domain.Products;

public sealed class Product : AggregateRoot<ProductId>, IFullAudited
{
    private Product()
    {
    }
    private Product(ProductId id, BrandId brandId, CategoryId categoryId, string title, string description, AverageRating averageRating, ProductStatus status,
       bool isSerialized, IReadOnlyDictionary<string, string> attributes, DateTime createdAt, DateTime lastModifiedAt, CustomerId createdBy, CustomerId lastModifiedBy)
        : base(id)
    {
        BrandId = brandId;
        CategoryId = categoryId;
        Title = title;
        Description = description;
        IsSerialized = isSerialized;
        Attributes = attributes;
        AverageRating = averageRating;
        Status = status;
        CreatedAt = createdAt;
        LastModifiedAt = lastModifiedAt;
        CreatedBy = createdBy;
        LastModifiedBy = lastModifiedBy;
    }
    public static Result<Product> Create(ProductId id, BrandId brandId, CategoryId categoryId, string title, string description,
        bool isSerialized, IReadOnlyDictionary<string, string>? attributes)
    {
        // Add domain validation logic here

        //defaults
        AverageRating averageRating = new (0);
        DateTime createdAt = DateTime.UtcNow;
        DateTime lastModifiedAt = createdAt;
        CustomerId lastModifiedBy = CustomerId.EmptyInstance;
        CustomerId createdBy = lastModifiedBy;
        ProductStatus status = ProductStatus.Archived;

        attributes ??= new Dictionary<string,string>();

        var product = new Product(id, brandId, categoryId, title, description, averageRating, 
            status, isSerialized, attributes, createdAt, lastModifiedAt, createdBy, lastModifiedBy);

        return product;
    }


    public CategoryId CategoryId { get; private init; }
    public BrandId BrandId { get; private init; }

    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;

    public AverageRating AverageRating { get; private init; } = null!;
    public ProductStatus Status { get; }
    public bool IsSerialized { get; private set; }

    public Guid CreatedBy { get; set; }
    public Guid LastModifiedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }


    Dictionary<string, string> _attributes = [];
    public IReadOnlyDictionary<string, string> Attributes { get { return _attributes.AsReadOnly(); } private set { _attributes = value is null ? [] :value.ToDictionary(); } }

    private List<ProductVariant> _variants = [];
    public IReadOnlyCollection<ProductVariant> Variants { get { return _variants.AsReadOnly(); } private set { _variants = value is null ? [] : value.ToList(); } }



    //public Result<Updated> UpdateProductImages(List<ProductImage> newImages)
    //{
    //    if (newImages.Count < ProductRules.MinImagesCount || newImages.Count > ProductRules.MaxImagesCount)
    //    {
    //        return ProductErrors.ImagesOutOfRange;
    //    }

    //    _images = newImages;
    //    EnsureImagesSortOrderIsSequential();
    //    return Result.Updated;
    //}
    //private void EnsureImagesSortOrderIsSequential()
    //{
    //    //byte sortOrder = 1;
    //    //foreach (var image in _images.OrderBy(i => i.SortOrder))
    //    //{
    //    //    image.UpdateSortOrder(sortOrder++);
    //    //}
    //}
}
