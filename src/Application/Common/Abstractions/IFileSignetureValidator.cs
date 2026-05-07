

using Microsoft.AspNetCore.Http;

namespace Application.Common.Abstractions;

public interface IFileSignetureValidator
{
    public bool Validate(IFormFile file);
}
