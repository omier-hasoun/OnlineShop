

namespace Domain.Products;

public sealed class Product : BaseEntity, IFullAudited, ISoftDeletable

{
    private Product()
    {
    }

    public static Result<Product> Create(ProductId id, string name, string description, string manufacturer, decimal defaultPrice)
    {
        return new Product
        {
            Id = id,
            Name = name,
            Description = description,
            DefaultPrice = defaultPrice,
            Manufacturer = manufacturer,
        };
    }


    public ProductId Id { get; private init; }
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string Manufacturer { get; private set; } = null!;
    public float? AverageRating { get; } = null;
    public decimal DefaultPrice { get; private set; }

    public ICollection<ProductReview> Reviews { get; private set; } = [];
    public bool IsDeleted { get; set; }
    public UserId CreatedBy { get; set; }
    public UserId LastModifiedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }

    public Result<Updated> ChangeDefaultPrice(decimal newPrice)
    {
        if (newPrice < ProductRules.MinDefaultPriceValue || newPrice > ProductRules.MaxDefaultPriceValue)
        {
            return ProductErrors.PriceOutOfRange;
        }

        DefaultPrice = newPrice;
        return Result.Updated;
    }

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
