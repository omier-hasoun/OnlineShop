
using Domain;

namespace Application.AdminPanelFeatures.Products.Commands.CreateVariant;

internal sealed class CreateVaraintCommandValidator : AbstractValidator<CreateVariantCommand>
{
    public CreateVaraintCommandValidator()
    {
        RuleFor(x => x.Product_Id).NotEmpty();

        RuleFor(x => x.Price).Must(x => x > 0);

        RuleFor(x => x.Height).Must(x => x > 0);

        RuleFor(x => x.Weight).Must(x => x > 0);

        RuleFor(x => x.Length).Must(x => x > 0);

        RuleFor(x => x.Width).Must(x => x > 0);


        RuleFor(x => x.Specifications).Must(x => x.Count <= ProductVariantRules.MaxNumberOfSpecifications);

        RuleFor(x => x.Images)
                              .Must(x => x.Count <= ProductVariantRules.MaxNumberOfImages);

        RuleForEach(x => x.Images).NotEmpty()
                                  .Must(image => image.File.Length <= ApplicationRules.Uploads.MaxImageSizeForProducts)
                                  .WithMessage($"A single image cannot cannot exceed 10 Mb");
                                  

    }
}
