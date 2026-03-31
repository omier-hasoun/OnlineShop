using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Abstractions
{
    public interface IDeletionMetadata
    {
        public UserId DeletedBy { get; set; }
        public DateTime DeletedAt { get; set; }

    }
}
