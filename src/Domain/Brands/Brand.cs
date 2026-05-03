

namespace Domain.Brands;

public sealed class Brand : AggregateRoot<BrandId>, IHasCreationTime
{
    private Brand(BrandId id, string name, string? description, string companyName, string skuName, string? logoUrl, DateTime createdAt, bool isActive) 
        : base(id)
    {
        Name = name;
        Description = description;
        CompanyName = companyName;
        SkuName = skuName;
        LogoUrl = logoUrl;
        CreatedAt = createdAt;
        IsActive = isActive;
    }

    public static Result<Brand> Create(BrandId id, string name, string? description, string companyName, string skuName,  string? logoUrl)
    {
        return new Brand(id, name, description, companyName, skuName, logoUrl, DateTime.UtcNow, isActive: true);
    }

    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string CompanyName { get; private set; }
    public string SkuName { get; private set; }
    public string? LogoUrl { get; private set; }
    public string Slug { get { return SkuName; } }
    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; set; }


    public Result<Success> SetAsActivated()
    {
        IsActive = true;
        return Result.Success;
    }
    public Result<Success> SetAsDeactivated()
    {
        IsActive = false;
        return Result.Success;
    }
}
