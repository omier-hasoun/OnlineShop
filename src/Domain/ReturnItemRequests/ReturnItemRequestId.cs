
namespace Domain.ReturnItemRequests;

public readonly record struct ReturnItemRequestId
{
    public Guid Value { get; init; }

    public ReturnItemRequestId(Guid value)
    {
        Value = value;
    }

    public Result<Success> IsValid()
    {
        if (Value.Version != 7)
        {
            return DomainErrors.ReturnItemRequests.ReturnItemRequestIdInvalid;
        }

        return Result.Success;
    }
    public override string ToString()
    {
        return Value.ToString();
    }

}
