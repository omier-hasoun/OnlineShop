using Application.Common.RequestModels;

namespace Application.AdminPanelFeatures.Products.Commands.CreateVariant;

public sealed record CreateVariantCommand 
(
long Product_Id,
decimal Price,
int Width,
int Height,
int Length,
int Weight,
string Sku,
string Slug,
string BarCode,
Dictionary<string, string> Specifications
) : IRequest<Result<long>>;

