
using Domain.Categories;

namespace Application.Features.Management.Categories.Dtos;

public sealed record class CategoryBasicDto
{
    public long Id { get; init; }
    public string Name { get; init; }

    public CategoryBasicDto(CategoryId id, string name)
    {
        Name = name;
        Id = id.Value;
    }

}
