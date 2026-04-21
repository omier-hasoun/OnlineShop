
using Domain.Customers;

namespace Domain.Common.Abstractions
{
    public interface IDeletionMetadata
    {
        public UserId DeletedBy { get; set; }
        public DateTime DeletedAt { get; set; }

    }
}
