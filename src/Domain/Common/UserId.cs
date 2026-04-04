using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common;

public readonly record struct UserId
{
    public Guid Value { get; init; }

    public static implicit operator Guid(UserId userId) => userId.Value;
    public static implicit operator UserId(Guid value) => new UserId(value);

    public UserId(Guid value)
    {
        if (value.Version != 7 || value == default)
            throw new ArgumentException("UserId is invalid.", nameof(value));

        Value = value;
    }

}
