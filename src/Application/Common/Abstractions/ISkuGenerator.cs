
namespace Application.Common.Abstractions;

public interface ISkuGenerator
{
    public string GenerateSku(string productTitle, string Category, string? Color, string? condition);
}
