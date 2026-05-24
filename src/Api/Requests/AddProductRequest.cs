using Application.Features.Management.ProductGroups.Dtos;

namespace Api.Requests;

public sealed record AddProductRequest(
double Price,
int Width,
int Height,
int Length,
int Weight,
string Sku,
string Slug,
string BarCode,
Dictionary<string, string> Specifications,
List<IFormFile>? Images,
List<ProductStockDto> StockPerWarehouse
);
