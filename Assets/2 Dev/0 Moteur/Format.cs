using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Format : MonoBehaviour
{
    public enum Axis { X = 0, Y = 1 }

    #region Singleton

    private static Format I { get; set; }

    private void Awake()
    {
        if (I != null)
        {
            Destroy(gameObject);
            return;
        }
        I = this;

        Init();
    }

    #endregion

    #region Global Members

    [SerializeField] private Vector2 referenceBounds = new Vector2(17.7f, 10);

    [SerializeField] private Vector2Int referenceResolution = new Vector2Int(1920, 1080);

    #endregion

    #region Static Members

    public static Vector2 Resolution { get; private set; }

    public static float Ratio { get; private set; }
    public static float ReferenceRatio { get; private set; }
    public static float RatioDiff { get; private set; }
    
    public static Vector2 ResolutionDelta { get; private set; }
    public static float ScaleFactor { get; private set; }

    public static Vector2 ScreenBounds { get; private set; }
    public static Vector2 RefDemiBounds { get; private set; }
    public static Vector2 DemiBounds { get; private set; }

    #endregion

    #region Initialization

    private void Init()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        Resolution = new Vector2(Screen.width, Screen.height);
        
        ReferenceRatio = (float)referenceResolution.x / referenceResolution.y;
        Ratio = Resolution.x / Resolution.y;
        RatioDiff = Ratio / ReferenceRatio;

        ResolutionDelta = new Vector2(Resolution.x / referenceResolution.x, Resolution.y / referenceResolution.y);
        ScaleFactor = Mathf.Min(ResolutionDelta.x, ResolutionDelta.y);

        RefDemiBounds = new Vector2(referenceBounds.x / 2, referenceBounds.y / 2);

        ScreenBounds = new Vector2(referenceBounds.x * RatioDiff, referenceBounds.y);
        DemiBounds = new Vector2(ScreenBounds.x / 2, ScreenBounds.y / 2);

        Debug.Log("Resolution : " + Resolution + "\n" +
            "Screen : width = " + Screen.width + " height = " + Screen.height + "\n" +
            "Ratio : " + Ratio + "\n" +
            "Ratio Diff : " + RatioDiff + "\n" +
            "Resolution Delta : " + ResolutionDelta + "\n" +
            "Scale Factor : " + ScaleFactor + "\n" +
            "Screen Bounds : " + ScreenBounds);
    }

    #endregion

    #region Size

    public static Vector2 ComputeSize(float relativeSize, float ratio)
    {
        float factor = relativeSize * ScaleFactor;
        return new Vector2(ratio * factor, factor);
    }

    #endregion

    #region Position

    public static Vector3 ComputePosition(Vector2 relativePosition)
    {
        return new Vector3(relativePosition.x * DemiBounds.x, relativePosition.y * DemiBounds.y, 0);
    }
    public static Vector3 ComputePosition(float relativeX, float relativeY)
    {
        return new Vector3(relativeX * DemiBounds.x, relativeY * DemiBounds.y, 0);
    }
    public static Vector3 ComputePositionFromScreenPosition(Vector2 screenPosition)
    {
        return ComputeNormalizedPosition(new Vector2(Mathf.InverseLerp(0, Resolution.x, screenPosition.x), Mathf.InverseLerp(0, Resolution.y, screenPosition.y)));
    }
    public static Vector2 ComputeRelativeDeltaFromScreenDelta(Vector2 screenDelta)
    {
        return new Vector2(screenDelta.x / Resolution.x, screenDelta.y / Resolution.y);
    }
    public static Vector3 ComputeNormalizedPosition(Vector2 normalizedPosition)
    {
        return new Vector3(normalizedPosition.x * ScreenBounds.x - DemiBounds.x, normalizedPosition.y * ScreenBounds.y - DemiBounds.y, 0);
    }
    public static Vector2 GetReferenceRelativePosition(Vector3 basePos)
    {
        float absX = Mathf.Abs(basePos.x);
        bool xNeg = basePos.x < 0;
        float absY = Mathf.Abs(basePos.y);
        bool yNeg = basePos.y < 0;
        float x = Mathf.InverseLerp(0, RefDemiBounds.x, absX) * (xNeg ? -1f : 1f);
        float y = Mathf.InverseLerp(0, RefDemiBounds.y, absY) * (yNeg ? -1f : 1f);

        return new Vector2(x, y);
    }
    public static Vector3 ComputeCorrectPosition(Vector3 basePosition)
    {
        if (RatioDiff == 1) return basePosition;
        return ComputePosition(GetReferenceRelativePosition(basePosition));
    }
    //public static Vector3 ComputeRelativePositionFromWorld(Vector3 worldPos)
    //{
    //    return new Vector3(Mathf.InverseLerp(-DemiBounds.x, DemiBounds.x, worldPos.x), Mathf.InverseLerp(-DemiBounds.y, DemiBounds.y, worldPos.y), 0);
    //}

    #endregion
}
