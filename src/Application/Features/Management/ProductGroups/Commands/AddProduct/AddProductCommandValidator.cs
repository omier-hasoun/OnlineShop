using Domain;

namespace Application.Features.Management.ProductGroups.Commands.AddProduct;

internal sealed class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();

        RuleFor(x => x.Price).Must(x => x > 0);

        RuleFor(x => x.Height).Must(x => x > 0);

        RuleFor(x => x.Weight).Must(x => x > 0);

        RuleFor(x => x.Length).Must(x => x > 0);

        RuleFor(x => x.Width).Must(x => x > 0);


        RuleFor(x => x.Specifications).Must(x => x.Count <= ProductRules.MaxNumberOfSpecifications);

        RuleForEach(x => x.Specifications).Must(x => x.Key.Length <= 50 && x.Value.Length <= 50 && !string.IsNullOrWhiteSpace(x.Key) && !string.IsNullOrWhiteSpace(x.Value) );





    }
}
