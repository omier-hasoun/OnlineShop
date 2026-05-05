
namespace Application.Features.Products.Dtos;

public sealed record ImageDto
{
    public string Image { get; init; } = null!;
    public byte SortOrder { get; init; } 

}
