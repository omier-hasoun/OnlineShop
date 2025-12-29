namespace Application.Features.Products.Delete;

public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<Deleted>>
{
    private readonly IAppDbContext _context;
    public DeleteProductCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Deleted>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {

        var product = _context.Products.FirstOrDefault(p => p.Id == request.ProductId);

        if (product == null)
        {
            return ProductApplicationErrors.ProductNotFound;
        }

        _context.Products.Remove(product);

        var deleteResult = await _context.SaveChangesAsync(cancellationToken);

        if (deleteResult == 0)
        {
            return ProductApplicationErrors.ProductDeletionFailed;
        }

        return Result.Deleted;
    }

}
