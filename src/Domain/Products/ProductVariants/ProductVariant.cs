
using Domain.Common.ValueObjects;

namespace Domain.Products.ProductVariants;

public sealed class ProductVariant : BaseEntity<ProductVariantId>, ISoftDeleted
{
    private ProductVariant()
    {
    }
    private ProductVariant(ProductVariantId id, ProductId productId, Money originalPrice, byte discountPercentage, Money discountPrice,
        int width, int height, int length, int weight, string sku, bool isDeleted, bool displayBaseProductImages, IReadOnlyDictionary<string, string> specifications)
        : base(id)
    {
        ProductId = productId;
                
        OriginalPrice = originalPrice;
        DiscountPercentage = discountPercentage;
        DiscountPrice = discountPrice;
            
        Width = width;
        Height = height;
        Length = length;
        Weight = weight;

        Sku = sku;

        IsDeleted = isDeleted;
        DisplayBaseProductImages = displayBaseProductImages;

        _specifications = specifications.ToDictionary();
    }

    public static Result<ProductVariant> Create(ProductVariantId id, ProductId productId, Money originalPrice,
        int width, int height, int length, int weight, string sku, bool displayBaseProductImages, IReadOnlyDictionary<string, string> specifications)
    {
        //defaults
        bool isDeleted = false;
        byte discountPercentage = 0;
        Money discountPrice = originalPrice;


        return new ProductVariant(id, productId, originalPrice, discountPercentage, discountPrice,
            width, height, length, weight, sku, isDeleted, displayBaseProductImages, specifications);
    }
    public ProductId ProductId { get; private set; }


    public int Width { get; private set; }
    public int Height { get; private set; }
    public int Length { get; private set; }
    public int Weight { get; private set; }

    public Money OriginalPrice { get; }
    public Money DiscountPrice { get; }
    public byte DiscountPercentage { get; private set;}

    public string Sku { get; private set; } = null!;
    public string Slug { get {  return Sku; }  }

    public bool IsDeleted { get; private set; }
    public bool DisplayBaseProductImages { get; private set; }


    private Dictionary<string, string> _specifications = [];
    public IReadOnlyDictionary<string, string> Specifications { get { return _specifications.AsReadOnly(); } private set { _specifications = value is null ?[] :value.ToDictionary(); } }

    private static decimal CalculateDiscountPrice(decimal originalPrice, byte discountPercentage = 0)
    {
        if (discountPercentage == 0)
            return originalPrice;
        return originalPrice * (100 - discountPercentage) / 100;
    }
}
