using System;
using System.Collections.Generic;
using System.Text;

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
}
