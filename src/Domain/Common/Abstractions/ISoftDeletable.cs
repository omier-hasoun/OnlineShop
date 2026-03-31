namespace Domain.Common.Abstractions;

/// <summary>
/// Inherit if an entity should be soft deleted
/// </summary>
public interface ISoftDeletable
{
    public bool IsDeleted { get;  set; }

}
