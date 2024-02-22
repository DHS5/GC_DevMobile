using System;
using UnityEngine;
using UnityEngine.Profiling;

public class UpdateManager : MonoBehaviour
{
    public static event Action<int, float, float> OnUpdate;
    public static event Action OnLateUpdate;
    public static event Action<int, float, float> OnFixedUpdate;

    public static int FrameIndex { get; private set; }
    public static float FixedDelta { get; private set; }
    public static float CurrentTime { get; private set; }
    
    private static float _lastTime;

    public static bool IsActive { get; set; } = true;

    private void Start()
    {
        FrameIndex = 0;
        FixedDelta = Time.fixedDeltaTime;
    }

    private void Update()
    {
        if (!IsActive) return;

        var deltaTime = Time.deltaTime;
        CurrentTime = Time.time;

        OnUpdate?.Invoke(FrameIndex, deltaTime, CurrentTime);
        FrameIndex++;
        if (GameManager.Instance.DebugMode && Time.time - _lastTime > 1)
        {
            _lastTime = Time.time;
            PlayerHUD.Instance.SetFPS(1 / deltaTime);
            var ramText = "Allocated Memory: " + (Profiler.GetTotalAllocatedMemoryLong() / 1048576) + "MB\nReserved Memory: " + (Profiler.GetTotalReservedMemoryLong() / 1048576) + "MB\nUnused Reserved Memory: " + (Profiler.GetTotalUnusedReservedMemoryLong() / 1048576) + "MB";
            PlayerHUD.Instance.SetRamUsage(ramText);
        }
        
    }

    private void LateUpdate()
    {
        if (!IsActive) return;

        OnLateUpdate?.Invoke();
    }

    private void FixedUpdate()
    {
        CurrentTime = Time.time;
        OnFixedUpdate?.Invoke(FrameIndex, FixedDelta, CurrentTime);
    }

    public static void Clear()
    {
        OnUpdate = null;
        OnFixedUpdate = null;
        OnLateUpdate = null;
    }
}