
using Application.Common.Dtos;

namespace Application.Common.Abstractions;

public interface IImageValidator
{
    public int MinWidth { get; set; }
    public int MinHeight { get; set; }

    /// <summary>
    /// max size in Bytes. 1024 * 1024 = 1mb.
    /// </summary>
    public int MaxSize { get; set; }

    public Result<Success> Validate(FileUploadDto file);
    public Result<Success> ValidateAll(IReadOnlyCollection<FileUploadDto> files);

}
