

namespace Shared.Results;

public readonly struct Success { };
public readonly struct Created { };
public readonly struct Updated { };
public readonly struct Deleted { };

/// <summary>
/// Provides predefined result markers used to signal the outcome of operations
/// that do not return a value but still need to indicate success states.
/// such as Success, Created, Deleted, Updated
/// instead of using the bool to represnt succeeded operation
/// </summary>
public static class Result
{
    public static Success Success => default;
    public static Created Created => default;
    public static Updated Updated => default;
    public static Deleted Deleted => default;
}



public readonly record struct Result<TValue> : IResult<TValue>
{

    private readonly List<Error>? _errors = [];
    public bool Succeeded => field;
    public bool Failed => !Succeeded;
    public List<Error> Errors => _errors!;
    public Error TopError => (_errors?.Count > 0) ? Errors[0] : default;

    public TValue Value
    {
        get
        {
            if (Succeeded)
            {
                return field!;
            }

            throw new InvalidResultAccessException();
        }
    }

    [JsonConstructor]// for serialization
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("")]
    public Result(TValue value, List<Error> errors, bool isSuccess)
    {
        Succeeded = isSuccess;
        _errors = errors;
        Value = value;

    }

    private Result(List<Error> errors)
    {
        if (errors is null || errors.Count == 0)
        {
            throw new ArgumentNullException("errors list is empty");
        }
        Succeeded = false;
        _errors = errors!;
        Value = default;
    }

    private Result(TValue value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        Value = value;
        Succeeded = true;
    }
    public TNextValue Match<TNextValue>(Func<TValue, TNextValue> OnSuccess, Func<List<Error>, TNextValue> OnError)
        => Succeeded ? OnSuccess(Value!) : OnError(Errors);

    public static implicit operator Result<TValue>(TValue value)
        => new(value: value);

    public static implicit operator Result<TValue>(Error error)
        => new(errors: [error]);

    public static implicit operator Result<TValue>(List<Error> errors)
        => new(errors: errors);
}
