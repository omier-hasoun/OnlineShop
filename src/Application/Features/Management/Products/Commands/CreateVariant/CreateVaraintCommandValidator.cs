using Domain;

namespace Application.Features.Management.Products.Commands.CreateVariant;

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


                                  

    }
}
