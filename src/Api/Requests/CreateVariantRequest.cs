namespace Api.Requests;

public sealed record CreateVariantRequest(
decimal Price,
int Width,
int Height,
int Length,
int Weight,
string Sku,
string Slug,
string BarCode,
Dictionary<string, string> Specifications
);
