
using Application.Common.RequestModels;

namespace Api.Controllers.Products.Requests;

public sealed record CreateVariantRequest(
long Product_Id,
decimal Price,
int Width,
int Height,
int Length,
int Weight,
string Sku,
string Slug,
string BarCode,
List<ProductVariantImageUpload> Images,
Dictionary<string, string> Specifications
);

