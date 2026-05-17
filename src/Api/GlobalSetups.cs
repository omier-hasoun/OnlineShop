namespace Api;

public static class GlobalSetups
{
    public static void Init()
    {
        NetVips.Cache.MaxFiles = 0;
        NetVips.Cache.MaxMem = 0;
        NetVips.NetVips.Concurrency = 1;

    }
}
