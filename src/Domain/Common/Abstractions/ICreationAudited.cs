
using Domain.Customers;

namespace Domain.Common.Abstractions;
public interface ICreationAudited
{
    public UserId CreatedBy { get; set; }
}
