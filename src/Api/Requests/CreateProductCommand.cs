namespace Api.Requests;

public sealed record CreateProductCommand(
double Price,
int Width,
int Height,
int Length,
int Weight,
string Sku,
string Slug,
string BarCode,
Dictionary<string, string> Specifications
);
