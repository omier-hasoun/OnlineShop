using Domain.Customers;

namespace Domain.Common.Abstractions;

public interface IModificationAudited 
{
    public UserId LastModifiedBy { get; set; }
}

