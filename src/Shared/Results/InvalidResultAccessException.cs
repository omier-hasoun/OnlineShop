namespace Shared.Results;

public sealed class InvalidResultAccessException : Exception
{
    public InvalidResultAccessException()
        : base("Cannot access the value of a failed Result. Ensure the Result indicates success before accessing its value.")
    {
    }
}
