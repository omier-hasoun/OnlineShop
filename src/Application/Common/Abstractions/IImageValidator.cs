
namespace Application.Common.Abstractions;

public interface IImageValidator
{
    public bool Validate(Stream fileStream, int minWidth, int minHeight);
}
