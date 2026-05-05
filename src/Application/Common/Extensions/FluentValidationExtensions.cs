
namespace Application.Common.Extensions;

public static class FluentValidationExtensions
{

    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(this IRuleBuilderOptions<T, TProperty> builder, Error error)
    {
        return builder.WithErrorCode(error.Code).WithMessage(error.Description);
    }
}
