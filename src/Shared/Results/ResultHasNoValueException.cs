namespace Shared.Results;

public sealed class ResultHasNoValueException : Exception
{
    public ResultHasNoValueException()
        : base("Cannot access the value of a failed Result. Ensure the Result indicates success before accessing its value.")
    {
    }
}
