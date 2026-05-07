

using Application;
using FileSignatures;
using Microsoft.AspNetCore.Http;
using NetVips;
using static Application.ApplicationRules;
namespace Infrastructure.LocalServices.FileValidationService;

internal sealed class FileValidator(IFileFormatInspector inspector) : IFileSignetureValidator
{
    private FileFormat? GetFileFormatViaFileSignature(Stream file)
    {
        return inspector.DetermineFileFormat(file);
    }
    public bool Validate(IFormFile file)
    {
        FileFormat? format = GetFileFormatViaFileSignature(file.OpenReadStream());

        if (format is null || !Uploads.AllowedImageMediaTypesList.Contains(format.MediaType))
            return false;


        var image = Image.NewFromStream(file.OpenReadStream(), access: Enums.Access.Sequential);

        if (image.Width < Uploads.MinWidth || image.Height < Uploads.MinHeight)
            return false;


        return true;
    }

}
