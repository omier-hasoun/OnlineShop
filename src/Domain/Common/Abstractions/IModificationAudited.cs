
namespace Domain.Common.Abstractions;

public interface IModificationAudited 
{
    public Guid LastModifiedBy { get; set; }
}

