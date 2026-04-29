namespace Domain.Common.Entities.Addresses;

public readonly record struct AddressId
{
    public long Value { get; init; }

    public AddressId(long value)
    {
        Value = value;
    }


    public Result<Success> IsValid()
    {
        if (this.Value <= 0)
        {
            return DomainErrors.Addresses.AddressIdInvalid;
        }

        return Result.Success;
    }
}
