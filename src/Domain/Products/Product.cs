
using Domain.Products.ValueObjects;

namespace Domain.Products;

public sealed class Product : AggregateRoot<ProductId>, IFullAudited
{
    private Product()
    {
    }
    private Product(ProductId id, BrandId brandId, CategoryId categoryId, string title, string description, ProductAverageRating averageRating, ProductStatus status,
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
    public static Result<Product> Create(ProductId id, BrandId brandId, CategoryId categoryId, string title, string description,
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

        var product = new Product(id, brandId, categoryId, title, description, averageRating, 
            status, isSerialized, attributes, createdAt, lastModifiedAt, createdBy, lastModifiedBy);

        return product;
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

    private List<ProductVariant> _variants = [];
    public IReadOnlyCollection<ProductVariant> Variants { get { return _variants.AsReadOnly(); } private set { _variants = value is null ? [] : value.ToList(); } }


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


    public Result<Success> AddVariant(ProductVariantId varaintId, Money price, int width, int height,
        int length, int weight, string sku, string slug, string barCode, Dictionary<string, string> specifications)
    {
        if(_variants.Count >= ProductRules.MaxNumberOfVariants)
        {
            return DomainErrors.Products.MaxNumberOfVariantsReached;
        }

        var createVariantResult = ProductVariant.Create(varaintId, Id, price, width, height, length, weight, sku, slug, barCode, specifications);

        if(createVariantResult.Failed)
        {
            return createVariantResult.Errors;
        }

        _variants.Add(createVariantResult.Value);

        return Result.Success;
    }


    public Result<Updated> UpdateVariantImages(ProductVariantId variantId, List<ProductImage> newImages)
    {
        var variant = _variants.FirstOrDefault(x => x.Id == variantId);

        if (variant is null)
            return DomainErrors.ProductVariantIdInvalid;

        var updateResult = variant.UpdateImages(newImages);

        if (updateResult.Failed)
        {
            return updateResult.Errors;
        }
       
        return Result.Updated;
    }

    public Result<Updated> ChangeStatus(ProductStatus status)
    {
        if (status == ProductStatus.Draft)
        {
            if (this.Status == ProductStatus.Draft)
                return Result.Updated;
            else
                return DomainErrors.InvalidStateTransition;
        }

        if (status == ProductStatus.Published)
        {
            return Publish();
        }

        if (status == ProductStatus.Unpublished)
        {
            return Unpublish();
        }

        if (status == ProductStatus.Archived)
        {
            return Archive();
        }

        return DomainErrors.InvalidStateTransition;
    }

    public Result<Updated> ChangeVariantStatus(ProductVariantId variantId, ProductStatus status)
    {
        var variant = _variants.FirstOrDefault(x => x.Id == variantId);

        if (variant is null)
        {
            return DomainErrors.ProductVariantIdInvalid;
        }

        if (status == ProductStatus.Draft)
        {
            if (variant.Status == ProductStatus.Draft)
                return Result.Updated;
            else
                return DomainErrors.InvalidStateTransition;
        }

        if (status == ProductStatus.Published)
        {
            return PublishVariant(variant);
        }

        if (status == ProductStatus.Unpublished)
        {
            return UnpublishVariant(variant);
        }

        //if (status == ProductStatus.Archived)
        //{
        //    return Archive();
        //}

        return DomainErrors.InvalidStateTransition;
    }



    private Result<Updated> PublishVariant(ProductVariant variant)
    {

        if (this.Status == ProductStatus.Archived)
            return DomainErrors.InvalidStateTransition;

        if (this.Status != ProductStatus.Published)
        {
            var publishProductResult = Publish();

            if (publishProductResult.Failed)
            {
                return publishProductResult.Errors;
            }
        }

        variant.Publish();

        return Result.Updated;
    }

    private Result<Updated> UnpublishVariant(ProductVariant variant)
    {
        if (this.Status == ProductStatus.Archived )
            return DomainErrors.InvalidStateTransition;

        if (this.Status == ProductStatus.Published)
            variant.Unpublish();
        else
            return DomainErrors.InvalidStateTransition;

        return Result.Updated;
    }








    private Result<Updated> Publish()
    {
        if (_variants.Count == 0)
        {
            return DomainErrors.Products.CannotPublishThisProductAtLeast1VariantRequired;
        }

        if (Status != ProductStatus.Draft && Status != ProductStatus.Unpublished)
        {
            return DomainErrors.InvalidStateTransition;
        }

        _variants.ForEach(x => x.Publish());
        Status = ProductStatus.Published;

        return Result.Updated;
    }

    private Result<Updated> Unpublish()
    {
        if (Status != ProductStatus.Published)
        {
            return DomainErrors.InvalidStateTransition;
        }

        _variants.ForEach(x => x.Unpublish());

        Status = ProductStatus.Unpublished;

        return Result.Updated;
    }
    private Result<Updated> Archive()
    {
        if (Status == ProductStatus.Archived)
        {
            return Result.Updated;
        }

        _variants.ForEach(x => x.Archive());
        Status = ProductStatus.Archived;

        return Result.Updated;
    }






    private static Result<Success> ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title) || !ValHelper.IsValidTextLength(title, ProductRules.MinTitleLength, ProductRules.MaxTitleLength))
        {
            return DomainErrors.Products.TitleInvalid;
        }

        return Result.Success;
    }

    private static Result<Success> ValidateDesciption(string description)
    {
        if (string.IsNullOrWhiteSpace(description) || !ValHelper.IsValidTextLength(description, ProductRules.MinDescriptionLength, ProductRules.MaxDescriptionLength))
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
