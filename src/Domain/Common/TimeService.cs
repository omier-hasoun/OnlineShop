namespace Domain.Common;

public static class TimeService
{
    private static TimeProvider _provider { get; set; } = TimeProvider.System;

    public static DateTimeOffset UtcNow => _provider.GetUtcNow();

    
}
