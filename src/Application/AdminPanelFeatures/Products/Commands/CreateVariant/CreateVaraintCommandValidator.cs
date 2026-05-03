
using Domain;

namespace Application.AdminPanelFeatures.Products.Commands.CreateVariant;

internal sealed class CreateVaraintCommandValidator : AbstractValidator<CreateVariantCommand>
{
    public CreateVaraintCommandValidator()
    {
        RuleFor(x => x.ProductId).Must(x => x.IsValid() == Result.Success)
                                 .WithErrorCode(DomainErrors.ProductIdInvalid.Code)
                                 .WithMessage(DomainErrors.ProductIdInvalid.Description);

        RuleFor(x => x.Specifications).NotEmpty()
                                      .WithErrorCode(DomainErrors.ProductVariants.AtleastOneSpecificationRequired.Code)
                                      .WithMessage(DomainErrors.ProductVariants.AtleastOneSpecificationRequired.Description)
                                      .Must(x => x?.Count <= ProductVariantRules.MaxNumberOfSpecifications)
                                      .WithErrorCode(DomainErrors.ProductVariants.MaxAllowedSpecificationsNumberExceeded.Code)
                                      .WithMessage(DomainErrors.ProductVariants.MaxAllowedSpecificationsNumberExceeded.Description);

        RuleFor(x => x.Images).NotEmpty()
                              .Must(x => x.Count <= ProductVariantRules.MaxNumberOfImages)
                              .WithErrorCode(DomainErrors.ProductVariants.ImagesOutOfRange.Code)
                              .WithMessage(DomainErrors.ProductVariants.ImagesOutOfRange.Description);

        RuleForEach(x => x.Images).NotNull()
                                  .WithErrorCode(ApplicationErrors.Validation.InvalidImage.Code)
                                  .WithMessage(ApplicationErrors.Validation.InvalidImage.Description)
                                  .Must(image => image.File.Length <= ApplicationRules.Uploads.MaxImageSizeForProducts)
                                  .WithErrorCode(ApplicationErrors.Validation.InvalidImageSize.Code)
                                  .WithMessage(ApplicationErrors.Validation.InvalidImageSize.Description);
                                  

    }
}
