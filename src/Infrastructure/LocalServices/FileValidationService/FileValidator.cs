
using FileSignatures;

namespace Infrastructure.LocalServices.FileValidationService;

internal sealed class FileValidator(IFileFormatInspector inspector) : IFileValidationService
{
    private FileFormat? GetFileFormatViaFileSignature(Stream file)
    {
        return inspector.DetermineFileFormat(file);
    }
    public bool ValidateAsync(Stream file, string[] shouldMatchAnyMediaType)
    {
        FileFormat? format = GetFileFormatViaFileSignature(file);

        if (format is null || !shouldMatchAnyMediaType.Contains(format.MediaType))
            return false;

        return true;
    }

}
