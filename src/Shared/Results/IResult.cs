namespace Shared.Results;

public interface IResult
{
    bool Succeeded { get; }
    bool Failed { get; }

}

public interface IResult<out TValue> : IResult
{
    List<Error>? Errors { get; }
    TValue Value { get; }

}
