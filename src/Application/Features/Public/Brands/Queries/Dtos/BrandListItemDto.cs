

using Domain.Brands;

namespace Application.Features.Public.Brands.Queries.Dtos;

public sealed record BrandListItemDto
{
    public BrandListItemDto(BrandId id, string name)
    {
        Id = id.Value;
        Name = name;
    }
    public Guid Id { get; }
    public string Name { get; }

}
