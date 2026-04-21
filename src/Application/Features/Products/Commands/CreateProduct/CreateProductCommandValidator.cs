namespace Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty()
                            .WithErrorCode(ProductErrors.TitleRequired.Code)
                            .WithMessage(ProductErrors.TitleRequired.Description)
                            .Length(ProductRules.MinTitleLength, ProductRules.MaxTitleLength)
                            .WithErrorCode(ProductErrors.TitleOutOfRange.Code)
                            .WithMessage(ProductErrors.TitleOutOfRange.Description);

        RuleFor(x => x.Description).NotEmpty()
                                   .WithErrorCode(ProductErrors.DescriptionRequired.Code)
                                   .WithMessage(ProductErrors.DescriptionRequired.Description)
                                   .Length(ProductRules.MinDescriptionLength, ProductRules.MaxDescriptionLength)
                                   .WithErrorCode(ProductErrors.DescriptionOutOfRange.Code)
                                   .WithMessage(ProductErrors.DescriptionOutOfRange.Description);

        //RuleFor(x => x.BrandId.Value).NotEmpty()
        //                             .WithErrorCode(ProductErrors.BrandRequired.Code)
        //                             .WithMessage(ProductErrors.BrandRequired.Description)
        //                             .Length(ProductRules.MinBrandLength, ProductRules.MaxBrandLength)
        //                             .WithErrorCode(ProductErrors.BrandOutOfRange.Code)
        //                             .WithMessage(ProductErrors.BrandOutOfRange.Description);

    }
}
