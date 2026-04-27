using System.Net.Mime;
using FluentValidation.Validators;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Features.Products.Commands.CreateProduct;

internal sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty()
                             .WithErrorCode(ProductErrors.TitleInvalid.Code)
                             .WithMessage(ProductErrors.TitleInvalid.Description)
                             .Length(ProductRules.MinTitleLength, ProductRules.MaxTitleLength)
                             .WithErrorCode(ProductErrors.TitleOutOfRange.Code)
                             .WithMessage(ProductErrors.TitleOutOfRange.Description);

        RuleFor(x => x.Description).NotEmpty()
                                   .WithErrorCode(ProductErrors.DescriptionInvalid.Code)
                                   .WithMessage(ProductErrors.DescriptionInvalid.Description)
                                   .Length(ProductRules.MinDescriptionLength, ProductRules.MaxDescriptionLength)
                                   .WithErrorCode(ProductErrors.DescriptionOutOfRange.Code)
                                   .WithMessage(ProductErrors.DescriptionOutOfRange.Description);

        RuleFor(x => x.BrandId).NotEmpty()
                               .Must(brandId => Guid.TryParse(brandId, out _))
                               .WithErrorCode(ProductErrors.BrandInvalid.Code)
                               .WithMessage(ProductErrors.BrandInvalid.Description);

        //RuleFor(x => x.Attributes).NotEmpty()
        //                          .WithErrorCode(ProductErrors.At.Code)
        //                          .WithMessage(ProductErrors.BrandInvalid.Description);

        //RuleFor(x => x.Images).NotEmpty()
        //                      .Must(value => value.Count >= ProductRules.MinNum && value.Count <= ProductRules.MaxNumberOfVariants)
        //                      .WithErrorCode(ProductErrors.ImagesOutOfRange.Code)
        //                      .WithMessage(ProductErrors.ImagesOutOfRange.Description);

        //RuleForEach(x => x.Images).NotNull()
        //                          .WithErrorCode(ProductApplicationErrors.InvalidImage.Code)
        //                          .WithMessage(ProductApplicationErrors.InvalidImage.Description)
        //                          .Must(image => image.SizeInBytes <= ProductApplicationRules.MaxImageSizeBytes)
        //                          .WithErrorCode(ProductApplicationErrors.InvalidImageSize.Code)
        //                          .WithMessage(ProductApplicationErrors.InvalidImageSize.Description);
                       

    }
}

