
using Domain.ProductGroups.ValueObjects;

namespace Domain.ProductGroups;

public sealed class ProductGroup : AggregateRoot<ProductGroupId>, IFullAudited
{
    private ProductGroup()
    {
    }
    private ProductGroup(ProductGroupId id, BrandId brandId, CategoryId categoryId, string title, string description, ProductAverageRating averageRating, ProductStatus status,
       bool isSerialized, Dictionary<string, string> attributes, DateTime createdAt, DateTime lastModifiedAt, Guid createdBy, Guid lastModifiedBy)
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
    public static Result<ProductGroup> Create(ProductGroupId id, BrandId brandId, CategoryId categoryId, string title, string description,
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
        var status = ProductStatus.Draft;

        var createdAt = DateTime.UtcNow;
        var lastModifiedAt = createdAt;

        var lastModifiedBy = Guid.Empty;
        var createdBy = lastModifiedBy;

        attributes ??= new Dictionary<string,string>();

        var productGroup = new ProductGroup(id, brandId, categoryId, title, description, averageRating, 
            status, isSerialized, attributes, createdAt, lastModifiedAt, createdBy, lastModifiedBy);

        return productGroup;
    }


    public CategoryId CategoryId { get; private set; }
    public BrandId BrandId { get; private set; }

    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;

    public ProductAverageRating AverageRating { get; private init; } = null!;
    public ProductStatus Status { get; private set; }
    public bool IsSerialized { get; private set; }

    public Guid CreatedBy { get; set; }
    public Guid LastModifiedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }


    Dictionary<string, string> _attributes = [];
    public IReadOnlyDictionary<string, string> Attributes { get { return _attributes.AsReadOnly(); } private set { _attributes = value is null ? [] :value.ToDictionary(); } }

    private List<Product> _products = [];
    public IReadOnlyCollection<Product> Products { get { return _products.AsReadOnly(); } private set { _products = value is null ? [] : value.ToList(); } }


    public Result<Updated> Update(BrandId? brandId, CategoryId? categoryId, string? title, string? description,
        bool? isSerialized, Dictionary<string, string>? attributes)
    {
        if (Status == ProductStatus.Archived)
            return DomainErrors.Products.UpdateNotAllowedOnArchivedProducts;

        List<Error> errors = new(7);

        if (brandId != null)
        {
            if(Status != ProductStatus.Draft)
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

        if (categoryId != null)
        {
            if (Status != ProductStatus.Draft)
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

        if (isSerialized != null )
        {
            if (Status != ProductStatus.Draft)
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

        BrandId = brandId ?? BrandId;


        return Result.Updated;
    }


    public Result<Success> AddProduct(ProductId productId, Money price, int width, int height,
        int length, int weight, string sku, string slug, string barCode, Dictionary<string, string> specifications)
    {
        if(_products.Count >= ProductGroupRules.MaxNumberOfVariants)
        {
            return DomainErrors.Products.MaxNumberOfVariantsReached;
        }

        var createVariantResult = Product.Create(productId, Id, price, width, height, length, weight, sku, slug, barCode, specifications);

        if(createVariantResult.Failed)
        {
            return createVariantResult.Errors;
        }

        _products.Add(createVariantResult.Value);

        return Result.Success;
    }


    public Result<Updated> UpdateProductImages(ProductId productId, List<ProductImage> newImages)
    {
        var product = _products.FirstOrDefault(x => x.Id == productId);

        if (product is null)
            return DomainErrors.ProductIdInvalid;

        var updateResult = product.UpdateImages(newImages);

        if (updateResult.Failed)
        {
            return updateResult.Errors;
        }
       
        return Result.Updated;
    }

    public Result<Updated> PublishProduct(ProductId productId)
    {
        var product = _products.FirstOrDefault(x => x.Id == productId);

        if (product is null)
            return DomainErrors.ProductIdInvalid;

        if (this.Status == ProductStatus.Archived)
        {
            return DomainErrors.InvalidStateTransition;
        }

        if (this.Status == ProductStatus.Unpublished || this.Status == ProductStatus.Draft)
        {
            this.Status = ProductStatus.Published;
        }
        product.Publish();

        
        return Result.Updated;
    }

    public Result<Updated> UnpublishProduct(ProductId productId)
    {
        var product = _products.FirstOrDefault(x => x.Id == productId);

        if (product is null)
            return DomainErrors.ProductIdInvalid;

        if (this.Status != ProductStatus.Published)
            return DomainErrors.InvalidStateTransition;

        product.Unpublish();
        return Result.Updated;
    }

    public Result<Updated> PublishGroup()
    {
        if (_products.Count == 0)
        {
            return DomainErrors.Products.AtLeastOneVariant;
        }

        if (Status != ProductStatus.Draft && Status != ProductStatus.Unpublished)
        {
            return DomainErrors.InvalidStateTransition;
        }

        Status = ProductStatus.Published;
        _products.ForEach(x => x.Publish());

        return Result.Updated;
    }

    public Result<Updated> UnpublishGroup()
    {
        if (Status != ProductStatus.Published)
        {
            return DomainErrors.InvalidStateTransition;
        }

        _products.ForEach(x => x.Unpublish());

        Status = ProductStatus.Unpublished;

        return Result.Updated;
    }
    public Result<Updated> ArchiveGroup()
    {
        if (Status == ProductStatus.Archived)
        {
            return Result.Updated;
        }

        _products.ForEach(x => x.Archive());
        Status = ProductStatus.Archived;

        return Result.Updated;
    }






    private static Result<Success> ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title) || !ValHelper.IsValidTextLength(title, ProductGroupRules.MinTitleLength, ProductGroupRules.MaxTitleLength))
        {
            return DomainErrors.Products.TitleInvalid;
        }

        return Result.Success;
    }

    private static Result<Success> ValidateDesciption(string description)
    {
        if (string.IsNullOrWhiteSpace(description) || !ValHelper.IsValidTextLength(description, ProductGroupRules.MinDescriptionLength, ProductGroupRules.MaxDescriptionLength))
        {
            return DomainErrors.Products.DescriptionInvalid;
        }

        return Result.Success;
    }

    private static Result<Success> ValidateAttributes(Dictionary<string,string> attributes)
    {
        if (attributes is null || attributes.Count == 0)
        {
            return DomainErrors.Products.AttributesInvalid;
        }

        return Result.Success;
    }
}
