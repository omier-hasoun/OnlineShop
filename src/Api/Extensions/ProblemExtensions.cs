using App = Shared.Results;
namespace Api.Extensions;

public static class ProblemExtensions
{
    public static IResult ToProblem(this List<App.Error> errors)
    {
        if (errors.Count == 0)
        {
            return Results.Problem();
        }

        if (errors.All(error => error.Type == App.ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        return Problem(errors[0]);
    }

    private static IResult Problem(App.Error error)
    {
        var statusCode = error.Type switch
        {
            App.ErrorType.Conflict => StatusCodes.Status409Conflict,
            App.ErrorType.Validation => StatusCodes.Status400BadRequest,
            App.ErrorType.NotFound => StatusCodes.Status404NotFound,
            App.ErrorType.Unauthorized => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Results.Problem(statusCode: statusCode, title: error.Description);
    }

    private static IResult ValidationProblem(List<App.Error> errors)
    {
        var errorsDict = errors.ToDictionary(e => e.Code, e => new[] { e.Description });

        var problemDetails = new ValidationProblemDetails(errorsDict)
        {
            Status = StatusCodes.Status400BadRequest
        };

        return Results.Json(problemDetails, statusCode: StatusCodes.Status400BadRequest);
    }
}
