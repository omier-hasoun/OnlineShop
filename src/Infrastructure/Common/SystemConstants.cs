namespace Infrastructure.Common;

public static class SystemConstants
{
    /// <summary>
    /// mainly used for background operations
    /// can be used when no userId was provided
    /// </summary>
    public static readonly Guid SystemId =
     Guid.Parse("system00-0000-0000-0000-000000000000");


}
