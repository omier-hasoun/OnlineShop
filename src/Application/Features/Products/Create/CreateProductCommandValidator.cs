
namespace Application.Features.Products.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty()
                            .WithErrorCode(ProductErrors.NameRequired.Code)
                            .WithMessage(ProductErrors.NameRequired.Description)
                            .Length(ProductRules.MinNameLength, ProductRules.MaxNameLength)
                            .WithErrorCode(ProductErrors.NameOutOfRange.Code)
                            .WithMessage(ProductErrors.NameOutOfRange.Description);

        RuleFor(x => x.Description).NotEmpty()
                                   .WithErrorCode(ProductErrors.DescriptionRequired.Code)
                                   .WithMessage(ProductErrors.DescriptionRequired.Description)
                                   .Length(ProductRules.MinDescriptionLength, ProductRules.MaxDescriptionLength)
                                   .WithErrorCode(ProductErrors.DescriptionOutOfRange.Code)
                                   .WithMessage(ProductErrors.DescriptionOutOfRange.Description);

        RuleFor(x => x.DefaultPrice).GreaterThan(ProductRules.MinDefaultPriceValue)
                             .LessThan(ProductRules.MaxDefaultPriceValue)
                             .WithErrorCode(ProductErrors.PriceOutOfRange.Code)
                             .WithMessage(ProductErrors.PriceOutOfRange.Description);

        RuleFor(x => x.Manufacturer).NotEmpty()
                                     .WithErrorCode(ProductErrors.MadeByCompanyRequired.Code)
                                     .WithMessage(ProductErrors.MadeByCompanyRequired.Description)
                                     .Length(ProductRules.MinManufacturerLength, ProductRules.MaxManufacturerLength)
                                     .WithErrorCode(ProductErrors.ManufacturerOutOfRange.Code)
                                     .WithMessage(ProductErrors.ManufacturerOutOfRange.Description);

        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(ProductRules.MinQuantityValue)
                                .WithErrorCode(ProductErrors.QuantityRequired.Code)
                                .WithMessage(ProductErrors.QuantityRequired.Description);

    }
}
