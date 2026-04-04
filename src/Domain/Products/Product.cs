

global using Domain.Products.ProductReviews;

namespace Domain.Products;

public sealed class Product : BaseEntity, IFullAudited, ISoftDeletable

{
    private Product()
    {
    }

    public static Result<Product> Create(ProductId id, string title, string description, string brand)
    {
        var product = new Product();
        return new Product
        {
            Id = id,
            Title = title,
            Description = description,
            Brand = brand,
            
        };
    }


    public ProductId Id { get; private init; }
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string Brand { get; private set; } = null!;
    public float AverageRating { get; private set; }
    public bool IsSerialized { get; private set; }

    private readonly List<ProductReview>? _reviews = [];
    public IReadOnlyCollection<ProductReview>? Reviews { get { return _reviews is null ? null: _reviews.AsReadOnly(); } } 


    public bool IsDeleted { get; set; }
    public UserId CreatedBy { get; set; }
    public UserId LastModifiedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }

    //public Result<Updated> UpdateProductImages(ICollection<ProductImage> newImages)
    //{
    //    if (newImages.Count < ProductRules.MinProductImagesCount || newImages.Count > ProductRules.MaxProductImagesCount)
    //    {
    //        return ProductErrors.ImagesOutOfRange;
    //    }
    //    EnsureImagesSortOrderIsSequential(ref newImages);
    //    ProductImages = newImages;
    //    return Result.Updated;
    //}
    //private static void EnsureImagesSortOrderIsSequential(ref ICollection<ProductImage> images)
    //{
    //    byte sortOrder = 1;
    //    foreach (var image in images.OrderBy(i => i.SortOrder))
    //    {
    //        image.UpdateSortOrder(sortOrder++);
    //    }
    //}
}
