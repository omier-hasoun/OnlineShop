
using System.Drawing;

namespace Domain.Products.ProductVariants;

public sealed class ProductVariant : BaseEntity, IFullAudited, ISoftDeletable
{
    private ProductVariant(ProductVariantId id, ProductId productId, UserId createdBy, UserId lastModifiedBy, decimal originalPrice, byte discountPercentage, decimal currentPrice,
        int width, int height, int length, int weight, string sku, bool isDeleted, Color? itemColor, ProductCondition condition, string? subTitle)
    {
        Id = id;
        ProductId = productId;
        CreatedBy = createdBy;
        LastModifiedBy = lastModifiedBy;
                
        OriginalPrice = originalPrice;
        DiscountPercentage = discountPercentage;
        CurrentPrice = currentPrice;
            
        Width = width;
        Height = height;
        Length = length;
        Weight = weight;

        Sku = sku;
        IsDeleted = isDeleted;

        ItemColor = itemColor;
        Condition = condition;

        SubTitle = subTitle;
    }

    public static Result<ProductVariant> Create(ProductVariantId id, ProductId productId,  decimal originalPrice, byte discountPercentage,
       int width, int height, int length, int weight, string sku, Color? itemColor, ProductCondition condition, string? subTitle)
    {


        return new ProductVariant(id, productId, default, default, originalPrice, discountPercentage,
            CalculateCurrentPrice(originalPrice, discountPercentage), width, height, length, weight, sku, isDeleted: false, itemColor, condition, subTitle);
    }
    public ProductVariantId Id { get; private set; }
    public ProductId ProductId { get; private set; }
    public UserId CreatedBy { get; set; }
    public UserId LastModifiedBy { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }

    public string? SubTitle { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int Length { get; private set; }
    public int Weight { get; private set; }

    public decimal OriginalPrice { get; private set; }
    public decimal CurrentPrice { get; private set; }

    public byte DiscountPercentage { get; private set;}

    public string Sku { get; private set; } = null!;
    public bool IsDeleted { get; set; }
    public Color? ItemColor { get; private set;  }
    public ProductCondition? Condition { get; private set; }

    public Product ProductInfo { get; private set; } = null!;

    private static decimal CalculateCurrentPrice(decimal originalPrice, byte discountPercentage = 0)
    {
        if (discountPercentage == 0)
            return originalPrice;
        return originalPrice * (100 - discountPercentage) / 100;
    }

    
}
