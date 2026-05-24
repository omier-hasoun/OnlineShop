
using Application.Common.Dtos;
using Application.Features.Management.ProductGroups.Dtos;

namespace Application.Features.Management.ProductGroups.Commands.AddProduct;

public sealed record AddProductCommand 
(
long ProductGroupId,

double Price,

int Width,
int Height,
int Length,
int Weight,

string Sku,
string Slug,
string BarCode,

Dictionary<string, string> Specifications,

List<FileUploadDto>? Images,

List<ProductStockDto>? StockPerWarehouse
) : IRequest<Result<long>>;

