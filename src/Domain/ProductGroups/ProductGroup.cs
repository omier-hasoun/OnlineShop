using Domain.ProductGroups.Events;

namespace Domain.ProductGroups;

public sealed class ProductGroup : AggregateRoot<ProductGroupId>, IFullAudited
{
    private ProductGroup()
    {
    }
    private ProductGroup(ProductGroupId id, BrandId brandId, string brandName, CategoryId categoryId, string categoryName, string title, string normalizedTitle, string description, ProductAverageRating averageRating, ProductGroupState status,
       bool isSerialized, Dictionary<string, string> attributes, DateTime createdAt, DateTime lastModifiedAt, Guid createdBy, Guid lastModifiedBy)
        : base(id)
    {
        BrandId = brandId;
        BrandName = brandName;
        CategoryId = categoryId;
        CategoryName = categoryName;
        Title = title;
        NormalizedTitle = normalizedTitle;
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
    public static Result<ProductGroup> Create(ProductGroupId id, BrandId brandId, string brandName, CategoryId categoryId, string categoryName, string title, string description,
        bool isSerialized, Dictionary<string, string> attributes)
    {
        // Add domain validation logic here
        var validationResult = Result.ValidateAll(
                            () => id.Validate(),
                            () => brandId.IsValid(),
                            () => categoryId.IsValid()
                            );
        //defaults
        var averageRating = ProductAverageRating.From(0).Value;
        var status = ProductGroupState.Draft;

        var createdAt = DateTime.UtcNow;
        var lastModifiedAt = createdAt;

        var lastModifiedBy = Guid.Empty;
        var createdBy = lastModifiedBy;

        attributes ??= [];
        title = title.Trim();

        var normalizedTitle = RegexHelper.Normalize(title);

        var productGroup = new ProductGroup(id, brandId, brandName, categoryId, categoryName, title, normalizedTitle, description, averageRating, 
            status, isSerialized, attributes, createdAt, lastModifiedAt, createdBy, lastModifiedBy);

        return productGroup;
    }


    public CategoryId CategoryId { get; private set; }
    public BrandId BrandId { get; private set; }
    public ProductId? FeaturedProductId { get; private set; }

    public string CategoryName { get; private set; } = null!;
    public string BrandName { get; private set; } = null!;

    public string Title { get; private set; } = null!;
    public string NormalizedTitle { get; private init; } = null!;
    public string Description { get; private set; } = null!;

    public ProductAverageRating AverageRating { get; private set; } = null!;
    public bool IsSerialized { get; private set; }

    public Guid CreatedBy { get; set; }
    public Guid LastModifiedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }


    Dictionary<string, string> _attributes = [];
    public IReadOnlyDictionary<string, string> Attributes { get { return _attributes.AsReadOnly(); } private set { _attributes = value is null ? [] :value.ToDictionary(); } }

    private List<Product> _products = [];
    public IReadOnlyCollection<Product> Products { get { return _products.AsReadOnly(); } private set { _products = value is null ? [] : value.ToList(); } }

    public Product? FeaturedProduct { get; private set; }
    public ProductGroupState Status { get; private set; }

    private bool CanTransitionTo(ProductGroupState newStatus)
    {
        return (Status, newStatus) switch
        {
            (ProductGroupState.Draft, ProductGroupState.Published) => true,
            (ProductGroupState.Draft, ProductGroupState.Archived) => true,
            (ProductGroupState.Published, ProductGroupState.Unpublished) => true,
            (ProductGroupState.Published, ProductGroupState.Archived) => true,
            (ProductGroupState.Published, ProductGroupState.Published) => true,

            (ProductGroupState.Unpublished, ProductGroupState.Published) => true,
            (ProductGroupState.Unpublished, ProductGroupState.Archived) => true,
            _ => false
        };

    }

    private bool CanModify() => Status != ProductGroupState.Archived;


    public Result<Updated> Update(BrandId? brandId, string? brandName, CategoryId? categoryId, string? categoryName, string? title, string? description,
        bool? isSerialized, Dictionary<string, string>? attributes)
    {
        if (!CanModify())
            return DomainErrors.Locked;

        List<Error> errors = new(7);

        if (brandId != null && brandName != null)
        {
            if (Status != ProductGroupState.Draft)
                errors.Add(DomainErrors.Products.CannotChangeBrandAfterPublish);
            else
            {
                var res = brandId.Value.IsValid();
                if (res.Failed)
                {
                    errors.Add(res.TopError);
                }
            }

        }

        if (categoryId != null && categoryName != null)
        {
            if (Status != ProductGroupState.Draft)
                errors.Add(DomainErrors.Products.CannotChangeCategoryAfterPublish);
            else
            {
                var res = categoryId.Value.IsValid();
                if (res.Failed)
                {
                    errors.Add(res.TopError);
                }
            }

        }

        if (title != null)
        {
            var res = ValidateTitle(title);

            if (res.Failed)
                errors.Add(res.TopError);
        }

        if (description != null)
        {
            var res = ValidateDesciption(description);

            if (res.Failed)
                errors.Add(res.TopError);
        }

        if (isSerialized != null)
        {
            if (Status != ProductGroupState.Draft)
                errors.Add(DomainErrors.Products.CannotUpdateIsSerializedAfterPublish);

        }

        if (attributes != null)
        {
            var res = ValidateAttributes(attributes);

            if (res.Failed)
                errors.Add(res.TopError);
        }

        if (errors.Count > 0)
            return errors;

        _attributes = attributes ?? _attributes;

        IsSerialized = isSerialized ?? IsSerialized;

        Description = description ?? Description;

        Title = title ?? Title;

        CategoryId = categoryId ?? CategoryId;
        CategoryName = categoryName ?? CategoryName;

        BrandId = brandId ?? BrandId;
        BrandName = brandName ?? BrandName;

        return Result.Updated;
    }

    public Result<Success> ApplyDiscount(ProductId productId, DateOnly discountExpiresOn, byte discountPercentage)
    {
        if (!CanModify())
            return DomainErrors.Locked;

        var product = _products.FirstOrDefault(x => x.Id == productId);

        if (product is null)
            return DomainErrors.ProductIdInvalid;

        var res = product.ApplyDiscount(discountExpiresOn, discountPercentage);

        if (res.Failed)
            return res;

        RaiseDomainEvent(new ProductDiscountedDomainEvent(productId));
        return res;
    }

    public bool ProductExists(ProductId productId)
    {
        return Products.FirstOrDefault(x => x.Id == productId) != default;
    }

    public Result<Success> AddProduct(ProductId productId, Money price, int width, int height,
        int length, int weight, string sku, string slug, string barCode, Dictionary<string, string> specifications)
    {
        if (!CanModify())
            return DomainErrors.Locked;

        if (_products.Count >= ProductGroupRules.MaxNumberOfVariants)
        {
            return DomainErrors.Products.MaxNumberOfVariantsReached;
        }

        var createVariantResult = Product.Create(productId, Id, price, width, height, length, weight, sku, slug, barCode, specifications);

        if (createVariantResult.Failed)
        {
            return createVariantResult.Errors;
        }

        _products.Add(createVariantResult.Value);

        RaiseDomainEvent(new ProductCreatedDomainEvent(productId));

        bool isFirstProductInGroup = _products.Count == 1;

        if (isFirstProductInGroup)
        {
            FeaturedProductId = productId;
        }

        return Result.Success;
    }


    public Result<Updated> AddProductImages(ProductId productId, List<string> fileNames)
    {
        if (!CanModify())
            return DomainErrors.Locked;

        var product = _products.FirstOrDefault(x => x.Id == productId);

        if (product is null)
            return DomainErrors.ProductIdInvalid;

        return product.AddImages(fileNames);
    }

    public Result<Deleted> RemoveProductImages(ProductId productId, List<string> fileNames)
    {
        if (!CanModify())
            return DomainErrors.Locked;

        var product = _products.FirstOrDefault(x => x.Id == productId);

        if (product is null)
            return DomainErrors.ProductIdInvalid;

        return product.RemoveImages(fileNames);
    }

    public Result<Success> UpdateProductImagesSortOrder(ProductId productId, IReadOnlyCollection<ProductImage> images)
    {
        if (!CanModify())
            return DomainErrors.Locked;

        var product = _products.FirstOrDefault(x => x.Id == productId);

        if (product is null)
            return DomainErrors.ProductIdInvalid;

        return product.UpdateImagesSortOrder(images);
    }



    public Result<Updated> PublishProduct(ProductId productId)
    {
        if(!CanModify())
            return DomainErrors.Locked;

        var product = _products.FirstOrDefault(x => x.Id == productId);

        if (product is null)
            return DomainErrors.ProductIdInvalid;

        var featuredProduct = _products.First(x => x.Id == FeaturedProductId);

        var res = product.Publish();

        if (res.Failed)
            return res.Errors;

        if (!featuredProduct.IsPublished())
        {
            ChangeFeaturedProduct(productId);
        }

        this.Status = ProductGroupState.Published;

        return Result.Updated;
    }

    public Result<Updated> UnpublishProduct(ProductId productId)
    {
        if (!CanModify())
            return DomainErrors.Locked;

        var product = _products.FirstOrDefault(x => x.Id == productId);

        if (product is null)
            return DomainErrors.ProductIdInvalid;



        var res = product.Unpublish();

        if (res.Failed)
            return res.Errors;

        // when unpublishing the featured product then make another published product the featured one.

        if (FeaturedProductId == productId)
        {
            var getAnyPublishedProduct = _products.FirstOrDefault(x => x.IsPublished());

            if (getAnyPublishedProduct is not null)
            {
                ChangeFeaturedProduct(productId);
            }
            else
            {
                this.Status = ProductGroupState.Unpublished;
            }
        }
           
        return Result.Updated;
    }

    public Result<Updated> PublishGroup()
    {
        if (!CanTransitionTo(ProductGroupState.Published))
        {
            return DomainErrors.InvalidStateTransition;
        }

        if (_products.Count == 0)
        {
            return DomainErrors.Products.CannotPublishWithoutAnyProduct;
        }

        Status = ProductGroupState.Published;
        _products.ForEach(x => x.Publish());

        return Result.Updated;
    }

    public Result<Updated> UnpublishGroup()
    {
        if (!CanTransitionTo(ProductGroupState.Unpublished))
        {
            return DomainErrors.InvalidStateTransition;
        }

        _products.ForEach(x => x.Unpublish());

        Status = ProductGroupState.Unpublished;

        return Result.Updated;
    }
    public Result<Updated> ArchiveGroup()
    {
        if (!CanTransitionTo(ProductGroupState.Archived))
        {
            return DomainErrors.InvalidStateTransition;
        }

        _products.ForEach(x => x.Archive());

        Status = ProductGroupState.Archived;

        RaiseDomainEvent(new ProductGroupArchivedDomainEvent(Id));
        return Result.Updated;
    }


    private void ChangeFeaturedProduct(ProductId productId)
    {
        FeaturedProductId = productId;
    }


    internal static Result<Success> ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title) || !ValHelper.IsValidTextLength(title, ProductGroupRules.MinTitleLength, ProductGroupRules.MaxTitleLength))
        {
            return DomainErrors.Products.TitleInvalid;
        }

        return Result.Success;
    }

    internal static Result<Success> ValidateDesciption(string description)
    {
        if (string.IsNullOrWhiteSpace(description) || !ValHelper.IsValidTextLength(description, ProductGroupRules.MinDescriptionLength, ProductGroupRules.MaxDescriptionLength))
        {
            return DomainErrors.Products.DescriptionInvalid;
        }

        return Result.Success;
    }

    internal static Result<Success> ValidateAttributes(Dictionary<string,string> attributes)
    {
        if (attributes is null || attributes.Count == 0)
        {
            return DomainErrors.Products.AttributesInvalid;
        }

        return Result.Success;
    }
}
