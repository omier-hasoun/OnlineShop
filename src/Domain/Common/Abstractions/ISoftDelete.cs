namespace Domain.Common.Abstractions;

/// <summary>
/// Inherit if an entity should be soft deleted
/// please provide only a private setter and interceptor will do the rest of the work :)
/// </summary>
public interface ISoftDelete
{
    public bool IsDeleted { get; }

}
