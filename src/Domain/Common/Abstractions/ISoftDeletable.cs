namespace Domain.Common.Abstractions;

/// <summary>
/// Inherit if an entity should be soft deleted
/// </summary>
public interface ISofDeletable
{
    public bool IsDeleted { get; set; }
    public Guid? DeletedBy { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }


}
