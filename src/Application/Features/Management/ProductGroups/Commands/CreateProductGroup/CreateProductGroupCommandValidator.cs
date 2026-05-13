
namespace Application.Features.Management.ProductGroups.Commands.CreateProductGroup;

internal sealed class CreateProductGroupCommandValidator : AbstractValidator<CreateProductGroupCommand>
{
    public CreateProductGroupCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty()
                             .Length(ProductGroupRules.MinTitleLength, ProductGroupRules.MaxTitleLength);


        RuleFor(x => x.Description).NotEmpty()
                                   .Length(ProductGroupRules.MinDescriptionLength, ProductGroupRules.MaxDescriptionLength);

        RuleFor(x => x.BrandId).NotEmpty();

        RuleFor(x => x.CategoryId).NotEmpty();

        RuleFor(x => x.Attributes).NotEmpty()
                                  .Must(x => x.Count <= ProductGroupRules.MaxNumberOfAttributes)
                                  .WithMessage($"Attributes cannot exceed {ProductGroupRules.MaxNumberOfAttributes} values");

        RuleForEach(x => x.Attributes)
                                    .Must(kv => kv.Key.Length <= 50 && !string.IsNullOrWhiteSpace(kv.Key) )
                                    .WithMessage("Invalid key. It cannot be empty or too long");

        RuleForEach(x => x.Attributes)
                                .Must(kv =>  kv.Value.Length <= 50! && string.IsNullOrWhiteSpace(kv.Value))
                                .WithMessage("Invalid value. It cannot be empty or too long");




    }
}

