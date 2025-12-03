namespace Infrastructure.Common;

public static class SystemService
{
    /// <summary>
    /// mainly used for background operations, but
    /// can also be for auditable entities or softdeletable entites if no userId provided
    /// </summary>
    public static readonly Guid SystemId =
     Guid.Parse("system00-0000-0000-0000-000000000000");
}
