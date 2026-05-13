

namespace Application.Features.Management.ProductsGroups.Commands.UpdateProductImages;

internal sealed class UpdateProductImagesCommandValidator : AbstractValidator<UpdateProductImagesCommand>
{


    public UpdateProductImagesCommandValidator()
    {
        
        RuleFor(x => x.Images)
                      .Must(x => x.Count <= ProductRules.MaxNumberOfImages)
                      .WithMessage("Images count cannot exceed 10");

        RuleForEach(x => x.Images).NotEmpty()
                                  .Must(image => image.File.Length <= ApplicationRules.Uploads.MaxImageSizeForProducts)
                                  .WithMessage($"A single image cannot cannot exceed 10 Mb");
    }


}
