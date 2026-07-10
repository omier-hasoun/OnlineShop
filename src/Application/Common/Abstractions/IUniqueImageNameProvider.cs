

namespace Application.Common.Abstractions;

public interface IUniqueImageNameProvider
{
    public string Generate();
    public string GenerateWithExtension(string extension);
    public List<string> GenerateMany(int count);
}
