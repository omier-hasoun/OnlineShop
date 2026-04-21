using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Abstractions;

public abstract class AggregateRoot<TId> : BaseEntity<TId> where TId : struct, IEquatable<TId>
{

    protected AggregateRoot(TId Id) : base(Id)
    {
    }
}
