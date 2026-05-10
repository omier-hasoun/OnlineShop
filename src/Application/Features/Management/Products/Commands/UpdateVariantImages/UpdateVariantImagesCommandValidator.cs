using Application.Common.Extensions;
using Application.Common.RequestModels;

namespace Application.Features.Management.Products.Commands.UpdateVariantImages;

internal sealed class UpdateVariantImagesCommandValidator : AbstractValidator<UpdateVariantImagesCommand>
{


    public UpdateVariantImagesCommandValidator()
    {
        
        RuleFor(x => x.Images)
                      .Must(x => x.Count <= ProductVariantRules.MaxNumberOfImages)
                      .WithMessage("Images count cannot exceed 10");

        RuleForEach(x => x.Images).NotEmpty()
                                  .Must(image => image.File.Length <= ApplicationRules.Uploads.MaxImageSizeForProducts)
                                  .WithMessage($"A single image cannot cannot exceed 10 Mb");
    }


}
