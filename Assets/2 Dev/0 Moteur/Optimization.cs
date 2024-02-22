using System;

public static class Optimization
{
    public static event Action OnBeforeGCCollect;

    public static void GCCollect()
    {
        OnBeforeGCCollect?.Invoke();

        GC.Collect();
    }
}
