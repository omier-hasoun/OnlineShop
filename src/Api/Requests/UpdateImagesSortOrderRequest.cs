
namespace Api.Requests;

public sealed record UpdateImagesSortOrderRequest(IReadOnlyCollection<ProductImageDto> Images);
