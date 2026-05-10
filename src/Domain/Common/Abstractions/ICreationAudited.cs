
namespace Domain.Common.Abstractions;
public interface ICreationAudited
{
    public Guid CreatedBy { get; set; }
}
