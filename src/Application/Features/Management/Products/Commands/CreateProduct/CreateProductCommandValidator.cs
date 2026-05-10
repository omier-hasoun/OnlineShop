using Application.Common.Extensions;
using Domain;
using Domain.Brands;
using Domain.Categories;
using FluentValidation.Validators;

namespace Application.Features.Management.Products.Commands.CreateProduct;

internal sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty()
                             .Length(ProductRules.MinTitleLength, ProductRules.MaxTitleLength);


        RuleFor(x => x.Description).NotEmpty()
                                   .Length(ProductRules.MinDescriptionLength, ProductRules.MaxDescriptionLength);

        RuleFor(x => x.Brand_Id).NotEmpty();

        RuleFor(x => x.Category_Id).NotEmpty();

        RuleFor(x => x.Attributes).NotEmpty()
                                  .Must(x => x.Count <= ProductRules.MaxNumberOfAttributes)
                                  .WithMessage($"Attributes cannot exceed {ProductRules.MaxNumberOfAttributes} values");

        RuleForEach(x => x.Attributes)
                                    .Must(kv => kv.Key.Length <= 50 && !string.IsNullOrWhiteSpace(kv.Key) )
                                    .WithMessage("Invalid key. It cannot be empty or too long");

        RuleForEach(x => x.Attributes)
                                .Must(kv =>  kv.Value.Length <= 50! && string.IsNullOrWhiteSpace(kv.Value))
                                .WithMessage("Invalid value. It cannot be empty or too long");




    }
}

