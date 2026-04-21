using Domain.Brands;
using Domain.Categories;

namespace Application.Features.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
BrandId BrandId, CategoryId CategoryId, string Title, string Description, decimal DefaultOriginalPrice, bool ContainsAlcohol, bool IsDangerousGood,
        bool IsBiological, bool IsSerialized, byte MaxQuantityPerCustomer, IReadOnlyDictionary<string, string>? Attributes

) : IRequest<Result<ProductId>>;
