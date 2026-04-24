using Domain.Brands;
using Domain.Categories;
using Domain.Common.ValueObjects;

namespace Application.Features.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
BrandId BrandId, CategoryId CategoryId, string Title, string Description, Money DefaultOriginalPrice, bool ContainsAlcohol, bool IsDangerousGood,
        bool IsBiological, bool IsSerialized, byte MaxQuantityPerCustomer, IReadOnlyList<string>? Attributes

) : IRequest<Result<ProductId>>;
