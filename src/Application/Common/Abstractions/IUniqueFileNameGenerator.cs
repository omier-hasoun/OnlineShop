

namespace Application.Common.Abstractions;

public interface IUniqueFileNameGenerator
{
    public string Generate();
    public string GenerateWithExtension(string extension);
    public List<string> GenerateMany(int count);
}
