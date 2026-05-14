namespace Api.Requests;

public sealed record AddCartItemRequest(long ProductId, short Quantity)
{

}
