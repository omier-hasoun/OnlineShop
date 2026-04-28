
namespace Domain.ReturnItemRequests;

public readonly record struct ReturnItemRequestId
{
    public Guid Value { get; init; }

    public static implicit operator Guid(ReturnItemRequestId orderReturnRequestId) => orderReturnRequestId.Value;
    public static implicit operator ReturnItemRequestId(Guid value) => new ReturnItemRequestId(value);
    public ReturnItemRequestId(Guid value)
    {
        if (value.Version != 7 || value == default)
            throw new ArgumentException("orderReturnRequestId is invalid.", nameof(value));

        Value = value;
    }

    public static ReturnItemRequestId Parse(string value)
    {
        if (TryParse(value, out var id))
            return id;
        throw new ArgumentException("ReturnItemRequestId is invalid.", nameof(value));
    }
    public static bool TryParse(string value, out ReturnItemRequestId id)
    {
        if (Guid.TryParse(value, out var brandId))
        {
            id = new ReturnItemRequestId(brandId);
            return true;
        }
        id = new();
        return false;
    }
}
