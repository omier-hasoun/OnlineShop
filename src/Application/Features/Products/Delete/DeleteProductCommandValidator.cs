

namespace Application.Features.Products.Delete;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithErrorCode(ProductErrors.ProductIdRequired.Code)
            .WithMessage(ProductErrors.ProductIdRequired.Description);

    }
}
