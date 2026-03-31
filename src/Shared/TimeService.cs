namespace Shared;

public static class TimeService
{
    private static TimeProvider _provider { get; set; } = TimeProvider.System;

    public static DateTime UtcNow => _provider.GetUtcNow().DateTime;

    
}
