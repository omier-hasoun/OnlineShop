
namespace Domain.Common.Exceptions;

public sealed class InvalidProductAverageRatingException : Exception
{
    public InvalidProductAverageRatingException(float value) : base($"{nameof(value)} was {value}. A productGroup rating must be between 0 and 5 stars")
    {
        
    }
}
