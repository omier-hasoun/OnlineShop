
using Application.Common.Dtos;

namespace Application.Features.Management.ProductGroups.Commands.AddProduct;

public sealed record AddProductCommand 
(
long ProductId,

double Price,

int Width,
int Height,
int Length,
int Weight,

string Sku,
string Slug,
string BarCode,

Dictionary<string, string> Specifications,

List<FileUploadDto>? Images

) : IRequest<Result<long>>;

