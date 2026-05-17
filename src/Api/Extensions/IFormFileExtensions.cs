
namespace Api.Extensions;

public static class IFormFileExtensions
{
    public static FileUploadDto ToDto(this IFormFile file, string internalFileName)
    {
        return new FileUploadDto
        {
            ContentLength = file.Length,
            ContentStream = file.OpenReadStream(),
            InternalFileName = internalFileName,
            MediaType = file.ContentType,
            OriginalFileName = file.FileName
        };
    }
}
