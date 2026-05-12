
using Domain.Brands;

namespace Application.Features.Management.Brands.Dtos;

public sealed record BrandBasicDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;

    public BrandBasicDto(BrandId id, string name)
    {
        Name = name;
        Id = id.Value;
    }

}
