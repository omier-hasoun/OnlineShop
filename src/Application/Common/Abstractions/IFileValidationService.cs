
namespace Application.Common.Abstractions;

public interface IFileValidationService
{
    public bool ValidateAsync(Stream file, string[] shouldMatchAnyMediaType);
}
