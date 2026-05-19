


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
            return InfrastructureErrors.Images.InvalidImage;

        FileFormat? format = inspector.DetermineFileFormat(fileStream);

        fileStream.Position = 0L;

        if (format is null)
            return InfrastructureErrors.Images.InvalidImageFormat;

        if (file.ContentLength > MaxSize)
        {
            return InfrastructureErrors.Images.InvalidImageSize;
        }


        using var image = Image.NewFromStream(fileStream, access: Enums.Access.Sequential);

        fileStream.Position = 0L;

        if (image.Width < MinWidth || image.Height < MinHeight)
            return InfrastructureErrors.Images.InvalidImageAspectRatio;


        return Result.Success;
    }

    public Result<Success> ValidateAll(IReadOnlyCollection<FileUploadDto> files)
    {
        ArgumentNullException.ThrowIfNull(files);

        List<Error> errors = new(files.Count);

        foreach(var file in files)
        { 
            var result = Validate(file);

            if (result.Failed)
                errors.Add(result.TopError.WithParameters(file.OriginalFileName));

        }

        return errors.Count == 0 ? Result.Success : errors;
    }
}
