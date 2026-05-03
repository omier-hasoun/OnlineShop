

namespace Application.Common.Abstractions;

public interface IUniqueFileNameGenerator
{
    public string Generate();

    public List<string> GenerateMany(int count);
}
