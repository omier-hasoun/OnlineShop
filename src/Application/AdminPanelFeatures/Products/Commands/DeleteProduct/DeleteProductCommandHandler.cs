
namespace Application.AdminPanelFeatures.Products.Commands.DeleteProduct;

internal sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<Deleted>>
{
    private readonly IAppDbContext _context;
    public DeleteProductCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Deleted>> Handle(DeleteProductCommand request, CancellationToken ct)
    {
        var productId = new ProductId(request.ProductId);

        var product = await _context.Products.Include(x => x.Variants).FirstOrDefaultAsync(x => x.Id == productId, ct);

        if (product is null)
        {
            return ApplicationErrors.NotFound.Product;
        }

        if (product.CanDelete())
        {
            _context.Products.Remove(product);
        }
        else
        {
            product.Archive();
        }

        await _context.SaveAsync(ct);

        return Result.Deleted;
    }

}
