
using Domain.Common.ValueObjects;
using Domain.Products.ValueObjects;

namespace Domain.Products.ProductVariants;

public sealed class ProductVariant : BaseEntity<ProductVariantId>
{
    private ProductVariant()
    {
    }
    private ProductVariant(ProductVariantId id, ProductId productId, Money originalPrice, Money priceNow, byte discountPercentage,  ProductStatus status,
        int width, int height, int length, int weight, string sku, string slug, string barCode, IReadOnlyDictionary<string, string> specifications)
        : base(id)
    {
        ProductId = productId;
                
        OriginalPrice = originalPrice;
        DiscountPercentage = discountPercentage;
        PriceNow = priceNow;
        Status = status;
        Width = width;
        Height = height;
        Length = length;
        Weight = weight;

        Sku = sku;
        Slug = slug;
        BarCode = barCode;

        _specifications = specifications.ToDictionary();
    }

    public static Result<ProductVariant> Create(ProductVariantId id, ProductId productId, Money originalPrice,
        int width, int height, int length, int weight, string sku, string slug, string barCode, IReadOnlyDictionary<string, string> specifications)
    {
        //defaults
        byte discountPercentage = 0;
        Money priceNow = originalPrice;
        ProductStatus status = ProductStatus.Archived;
        
        return new ProductVariant(id, productId, originalPrice, priceNow, discountPercentage, status,
            width, height, length, weight, sku, slug, barCode, specifications);
    }

    public ProductId ProductId { get; private set; }

    public int Width { get; private set; }
    public int Height { get; private set; }
    public int Length { get; private set; }
    public int Weight { get; private set; }

    /// <summary>
    /// Product's original Price, used only to show the orginal price before discount
    /// </summary>
    public Money OriginalPrice { get; } = null!;

    /// <summary>
    /// Product's price now, used in checkout
    /// </summary>
    public Money PriceNow { get; } = null!;
    public ProductStatus Status { get; private set; }
    public byte DiscountPercentage { get; private set;}

    public string Sku { get; private set; } = null!;
    public string Slug { get; private set; } = null!;
    public string BarCode { get; }

    private List<ProductImage> _images = [];
    public IReadOnlyCollection<ProductImage> Images { get { return _images.AsReadOnly(); } private set { _images = value is null ? [] : value.ToList(); } }

    private Dictionary<string, string> _specifications = [];
    public IReadOnlyDictionary<string, string> Specifications { get { return _specifications.AsReadOnly(); } private set { _specifications = value is null ?[] :value.ToDictionary(); } }

    //private static decimal CalculateDiscountPrice(decimal originalPrice, byte discountPercentage = 0)
    //{
    //    if (discountPercentage == 0)
    //        return originalPrice;
    //    return originalPrice * (100 - discountPercentage) / 100;
    //}

}
