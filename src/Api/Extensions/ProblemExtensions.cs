using app = Shared.Results;
namespace Api.Extensions;

public static class ProblemExtensions
{
    public static IResult ToProblem(this List<app.Error> errors)
    {
        if (errors.Count == 0)
        {
            return Results.Problem();
        }

        if (errors.All(error => error.Type == app.ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        return Problem(errors[0]);
    }

    private static IResult Problem(app.Error error)
    {
        var statusCode = error.Type switch
        {
            app.ErrorType.Conflict => StatusCodes.Status409Conflict,
            app.ErrorType.Validation => StatusCodes.Status400BadRequest,
            app.ErrorType.NotFound => StatusCodes.Status404NotFound,
            app.ErrorType.Unauthorized => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Results.Problem(statusCode: statusCode, title: error.Description);
    }

    private static IResult ValidationProblem(List<app.Error> errors)
    {
        var errorsDict = errors.ToDictionary(e => e.Code, e => new[] { e.Description });

        var problemDetails = new ValidationProblemDetails(errorsDict)
        {
            Status = StatusCodes.Status400BadRequest
        };

        return Results.Json(problemDetails, statusCode: StatusCodes.Status400BadRequest);
    }
}
