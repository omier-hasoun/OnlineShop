using System.ComponentModel;

namespace Domain.Common.Abstractions;

public abstract class AuditableEntity : BaseEntity
{
    protected AuditableEntity() : base()
    {

    }
    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset LastModifiedAt { get; private set; }
    public Guid LastModifiedBy { get; private set; }


    /// <summary>
    /// No need to call this method outside the Interceptor!!!
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="utcNow"></param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void _setCreated(Guid userId, DateTimeOffset utcNow = default)
    {
        CreatedBy = userId;
        CreatedAt = utcNow == default ? DateTimeOffset.UtcNow : utcNow;

        // the creator is the last one who modified it
        _setModified(CreatedBy, CreatedAt);
    }

    /// <summary>
    /// No need to call this method outside the Interceptor!!!
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="utcNow"></param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void _setModified(Guid userId, DateTimeOffset utcNow)
    {
        LastModifiedBy = userId;
        LastModifiedAt = utcNow == default ? DateTimeOffset.UtcNow : utcNow;
    }
}
