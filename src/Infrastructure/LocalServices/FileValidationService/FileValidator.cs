


using FileSignatures;
using NetVips;
using static Application.ApplicationRules;
namespace Infrastructure.LocalServices.FileValidationService;

internal sealed class FileValidator(IFileFormatInspector inspector) : IImageValidator
{
    private FileFormat? GetFileFormatViaFileSignature(Stream file)
    {
        return inspector.DetermineFileFormat(file);
    }

    public bool Validate(Stream fileStream, int minWidth, int minHeight)
    {
        FileFormat? format = GetFileFormatViaFileSignature(fileStream);

        if (format is null || !Uploads.AllowedImageMediaTypesList.Contains(format.MediaType))
            return false;

        using var image = Image.NewFromStream(fileStream, access: Enums.Access.Sequential);

        fileStream.Position = 0L;

        if (image.Width < minWidth || image.Height < minHeight)
            return false;


        return true;
    }
}
