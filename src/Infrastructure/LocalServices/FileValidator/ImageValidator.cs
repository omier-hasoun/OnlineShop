


using Application.Common.Dtos;
using FileSignatures;
using NetVips;

namespace Infrastructure.LocalServices.FileValidator;

internal sealed class ImageValidator(IFileFormatInspector inspector) : IImageValidator
{
    public int MinWidth { get; set; } = 800;
    public int MinHeight { get; set; } = 800;
    public int MaxSize { get; set; } = 20 * 1024 * 1024;

    public Result<Success> Validate(FileUploadDto file)
    {
        ArgumentNullException.ThrowIfNull(file);

        Stream fileStream = file.ContentStream;

        if (fileStream.CanSeek is false)
            return TechnicalErrors.Images.InvalidImage;

        FileFormat? format = inspector.DetermineFileFormat(fileStream);

        fileStream.Position = 0L;

        if (format is null)
            return TechnicalErrors.Images.InvalidImageFormat;

        if (file.ContentLength > MaxSize)
        {
            return TechnicalErrors.Images.InvalidImageSize;
        }


        using var image = Image.NewFromStream(fileStream, access: Enums.Access.Sequential);

        fileStream.Position = 0L;

        if (image.Width < MinWidth || image.Height < MinHeight)
            return TechnicalErrors.Images.InvalidImageAspectRatio;


        return Result.Success;
    }

    public Result<Success> ValidateAll(List<FileUploadDto> files)
    {
        ArgumentNullException.ThrowIfNull(files);

        List<Error> errors = new(files.Count);

        files.ForEach(file =>
        {
            var result = Validate(file);

            if (result.Failed)
                errors.Add(result.TopError.WithParameters(file.OriginalFileName));

        });

        return errors.Count == 0 ? Result.Success : errors;
    }
}
