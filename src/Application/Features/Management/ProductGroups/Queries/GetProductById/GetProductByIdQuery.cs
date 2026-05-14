
using Application.Features.Management.ProductGroups.Dtos;
using Domain.ProductsGroups.Products;

namespace Application.Features.Management.ProductGroups.Queries.GetProductById;

public sealed record GetProductByIdQuery(long ProductId) : IRequest<ProductDto>
{
    public ProductId ParsedProductId => new (ProductId);
}
