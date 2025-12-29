
using Domain.Common.ValidationRules;

namespace Application.Features.Products.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty()
                            .WithErrorCode(ProductErrors.NameRequired.Code)
                            .WithMessage(ProductErrors.NameRequired.Description)
                            .Length(ProductRules.NameMinLength, ProductRules.NameMaxLength)
                            .WithErrorCode(ProductErrors.NameOutOfRange.Code)
                            .WithMessage(ProductErrors.NameOutOfRange.Description);

        RuleFor(x => x.Description).NotEmpty()
                                   .WithErrorCode(ProductErrors.DescriptionRequired.Code)
                                   .WithMessage(ProductErrors.DescriptionRequired.Description)
                                   .Length(ProductRules.DescriptionMinLength, ProductRules.DescriptionMaxLength)
                                   .WithErrorCode(ProductErrors.DescriptionOutOfRange.Code)
                                   .WithMessage(ProductErrors.DescriptionOutOfRange.Description);

        RuleFor(x => x.Price).GreaterThan(ProductRules.PriceMinValue)
                             .LessThan(ProductRules.PriceMaxValue)
                             .WithErrorCode(ProductErrors.PriceOutOfRange.Code)
                             .WithMessage(ProductErrors.PriceOutOfRange.Description);

        RuleFor(x => x.MadeByCompany).NotEmpty()
                                     .WithErrorCode(ProductErrors.MadeByCompanyRequired.Code)
                                     .WithMessage(ProductErrors.MadeByCompanyRequired.Description)
                                     .Length(ProductRules.MadeByCompanyMinLength, ProductRules.MadeByCompanyMaxLength)
                                     .WithErrorCode(ProductErrors.MadeByCompanyOutOfRange.Code)
                                     .WithMessage(ProductErrors.MadeByCompanyOutOfRange.Description);

        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0)
                                .WithErrorCode(ProductErrors.QuantityRequired.Code)
                                .WithMessage(ProductErrors.QuantityRequired.Description);

    }
}
