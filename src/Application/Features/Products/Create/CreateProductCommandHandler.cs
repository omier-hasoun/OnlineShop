namespace Application.Features.Products.Create;

public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductId>>
{
    private readonly IAppDbContext _context;
    public CreateProductCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ProductId>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (ProductNameAlreadyExists(request.Name))
        {
            return ProductApplicationErrors.ProductNameAlreadyExists;
        }

        var createProductResult = Product.Create(
            request.Name,
            request.Description,
            request.MadeByCompany,
            request.Price,
            request.Quantity
        );

        if (createProductResult.Failed)
        {
            return createProductResult.Errors;
        }

        _context.Products.Add(createProductResult.Value);

        var result = await _context.SaveChangesAsync(cancellationToken);

        return result > 0
            ? createProductResult.Value.Id
            : ProductApplicationErrors.ProductCreationFailed;
    }

    private bool ProductNameAlreadyExists(string name)
    {
        return _context.Products.Any(p => p.Name == name);
    }
}
