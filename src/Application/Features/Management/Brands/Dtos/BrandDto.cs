
using Domain.Brands;

namespace Application.Features.Management.Brands.Dtos;

public sealed record BrandDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;

    public string CompanyName { get; init; } = null!;
    public string? Description { get; }
    public string SkuName { get; }
    public string? LogoUrl { get; }
    public DateTime CreatedAt { get; }
    public bool IsActive { get; }

    public BrandDto(BrandId id, string name, string companyName, string? description, string skuName, string? logoUrl, DateTime createdAt, bool isActive)
    {
        
        Name = name;
        CompanyName = companyName;
        Description = description;
        Id = id.Value;
        SkuName = skuName;
        LogoUrl = logoUrl;
        CreatedAt = createdAt;
        IsActive = isActive;
    }

}
