
using Domain.Common.ValueObjects;
using Domain.Products.ValueObjects;

namespace Domain.Products;

public sealed class Product : AggregateRoot<ProductId>, IFullAudited
{
    private Product()
    {
    }
    private Product(ProductId id, BrandId brandId, CategoryId categoryId, string title, string description, AverageRating averageRating, ProductStatus status,
       bool isSerialized, IReadOnlyDictionary<string, string> attributes, DateTime createdAt, DateTime lastModifiedAt, Guid createdBy, Guid lastModifiedBy)
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
        var averageRating = new AverageRating();//0

        var createdAt = DateTime.UtcNow;
        var lastModifiedAt = createdAt;
        var lastModifiedBy = Guid.Empty;
        var createdBy = lastModifiedBy;
        var status = ProductStatus.NotActive;

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


    public Result<Success> AddVariant(ProductVariantId varaintId, Money price, int width, int height,
        int length, int weight, string sku, string slug, string barCode, IReadOnlyDictionary<string, string> specifications)
    {

        var createVariantResult = ProductVariant.Create(varaintId, Id, price, width, height, length, weight, sku, slug, barCode, specifications);

        if(createVariantResult.Failed)
        {
            return createVariantResult.Errors;
        }

        _variants.Add(createVariantResult.Value);
        return Result.Success;
    }


    public Result<Updated> UpdateVariantImages(ProductVariantId varaintId, List<ProductImage> newImages)
    {
        var variant = _variants.FirstOrDefault(x => x.Id == varaintId);

        if (variant is null)
            return DomainErrors.ProductVariantIdInvalid;

        var updateResult = variant.UpdateImages(newImages);

        if (updateResult.Failed)
        {
            return updateResult.Errors;
        }
       
        return Result.Updated;
    }

}
