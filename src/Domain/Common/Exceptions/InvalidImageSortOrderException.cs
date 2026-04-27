
namespace Domain.Common.Exceptions;

public sealed class InvalidImageSortOrderException : Exception
{
    public InvalidImageSortOrderException() : base("Image sort order must be between 1 and 200")
    {
        
    }
}
