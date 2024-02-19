using System;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public static event Action<int, float, float> OnUpdate;
    public static event Action OnLateUpdate;
    public static event Action OnFixedUpdate;

    public static int FrameIndex;

    private void Start()
    {
        FrameIndex = 0;
    }

    private void Update()
    {
        FrameIndex++;
        float deltaTime = Time.deltaTime;
        float time = Time.time;

        OnUpdate?.Invoke(FrameIndex, deltaTime, time);
    }

    private void LateUpdate()
    {
        OnLateUpdate?.Invoke();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }
}
