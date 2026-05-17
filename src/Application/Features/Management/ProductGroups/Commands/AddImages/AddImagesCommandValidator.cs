

namespace Application.Features.Management.ProductGroups.Commands.AddImages;

internal sealed class AddImagesCommandValidator : AbstractValidator<AddImagesCommand>
{


    public AddImagesCommandValidator()
    {
        
        RuleFor(x => x.Images)
                      .Must(x => x.Count <= ProductRules.MaxNumberOfImages)
                      .WithMessage($"A product can have only up to {ProductRules.MaxNumberOfImages} images");

        RuleForEach(x => x.Images).NotEmpty()
                                  .Must(image => image.ContentLength <= ApplicationRules.Uploads.MaxProductImageSize)
                                  .WithMessage($"An image cannot cannot exceed {ApplicationRules.Uploads.MaxProductImageSize} Mb");
    }


}
