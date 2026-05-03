

using Microsoft.AspNetCore.Http;

namespace Application.Common.Abstractions;

public interface IFileValidationService
{
    public Result<Success> Validate(IFormFile file);
}
