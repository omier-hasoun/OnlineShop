namespace Api.Controllers.Products.Requests;

public sealed class ListProductsRequest
{
    public required int PageNumber { get; init; }
    public required int PageSize { get; init; }
    public int? MaxPrice { get; init; }
    public string? SearchText { get; init; }
    public long? CategoryId { get; init; }
    public Guid? BrandId { get; init; }


}
