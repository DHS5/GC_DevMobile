using System;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public static event Action<int, float, float> OnUpdate;
    public static event Action OnLateUpdate;
    public static event Action<int, float, float> OnFixedUpdate;

    public static int FrameIndex { get; private set; }
    public static float FixedDelta { get; private set; }
    public static float CurrentTime { get; private set; }

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