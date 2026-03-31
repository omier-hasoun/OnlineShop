using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Abstractions;
public interface ICreationAudited
{
    public UserId CreatedBy { get; set; }
}
