
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

        var product = _context.Products.FirstOrDefault(p => p.Id == request.ProductId);

        if (product is null)
        {
            //return ProductApplicationErrors.ProductNotFound;
        }

        _context.Products.Remove(product);

        await _context.SaveAsync(ct);


        //return deleteResult.Succeeded ? Result.Deleted : 
        //     ProductApplicationErrors.ProductDeletionFailed;
        throw new NotImplementedException();
    }

}
