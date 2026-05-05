namespace Shared.Results;

public record struct Error
{
    public string Code { get; }
    public string Description { get; set; }
    public ErrorType Type { get; }

    public object[]? Parameters { get; private set; } = null;

    private Error(string code, string description, ErrorType errorType)
    {
        if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentNullException("Please provide a valid code and description");
        }
        this.Type = errorType;
        this.Description = description;
        this.Code = code;
    }

    public Error WithParameters(params object[]?  parameters)
    {
        Parameters = parameters;
        return this;
    }

    public static Error Failure(string code, string description = "General failure.")
    => new(code, description, ErrorType.Failure);

    public static Error Unexpected(string code, string description = "Unexpected error.")
    => new(code, description, ErrorType.Unexpected);

    public static Error Unauthorized(string code, string description = "Unauthorized.")
    => new(code, description, ErrorType.Unauthorized);

    public static Error Validation(string code, string description = "Validation error.")
    => new(code, description, ErrorType.Validation);

    public static Error Forbidden(string code, string description = "Forbidden operation.")
    => new(code, description, ErrorType.Forbidden);

    public static Error Conflict(string code, string description = "Conflict error.")
    => new(code, description, ErrorType.Conflict);

    public static Error NotFound(string code, string description = "Not Found error.")
    => new(code, description, ErrorType.NotFound);

    public static Error ActionRequired(string code, string description = "An Action is required.")
    => new(code, description, ErrorType.Failure);
}
