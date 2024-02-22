using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Format : MonoBehaviour
{
    public enum Axis
    {
        X = 0,
        Y = 1
    }

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

    [SerializeField] private Vector2 referenceBounds = new(17.7f, 10);

    [SerializeField] private Vector2Int referenceResolution = new(1920, 1080);

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

    private static Dictionary<Vector2, Vector2> precomputedVectors = new();

    #endregion

    #region Initialization

    private void Init()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;

        Resolution = new Vector2(Screen.width, Screen.height);

        ReferenceRatio = (float)referenceResolution.x / referenceResolution.y;
        Ratio = Resolution.x / Resolution.y;
        RatioDiff = Ratio / ReferenceRatio;

        ResolutionDelta = new Vector2(Resolution.x / referenceResolution.x, Resolution.y / referenceResolution.y);
        if (RatioDiff >= 1f)
        {
            ScaleFactor = RatioDiff;
        }
        else
        {
            ScaleFactor = Mathf.Min(ResolutionDelta.x, ResolutionDelta.y);
        }
        //if (ResolutionDelta.x >= 1f && ResolutionDelta.y >= 1f)
        //{
        //    if (RatioDiff >= 1f)
        //        ScaleFactor = Mathf.Min(ResolutionDelta.x, ResolutionDelta.y, RatioDiff);
        //    else
        //        ScaleFactor = Mathf.Min(ResolutionDelta.x, ResolutionDelta.y);
        //}
        //else
        //{
        //    ScaleFactor = RatioDiff;
        //    //float resDeltaXDiff = Mathf.Abs(ResolutionDelta.x - 1);
        //    //float resDeltaYDiff = Mathf.Abs(ResolutionDelta.y - 1);
        //    //float ratioDiffDiff = Mathf.Abs(RatioDiff - 1);
        //    //ScaleFactor = resDeltaXDiff < resDeltaYDiff ?
        //    //    (resDeltaXDiff < ratioDiffDiff ? ResolutionDelta.x : RatioDiff) :
        //    //    (resDeltaYDiff < ratioDiffDiff ? ResolutionDelta.y : RatioDiff);
        //}

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
        var factor = relativeSize * ScaleFactor;
        return new Vector2(ratio * factor, factor);
    }

    #endregion

    #region Position

    public static Vector3 ComputePosition(Vector2 relativePosition)
    {
        return new Vector3(relativePosition.x * DemiBounds.x, relativePosition.y * DemiBounds.y, 0);
    }

    public static Vector2 ComputeVector2Position(Vector2 relativePosition)
    {
        return new Vector2(relativePosition.x * DemiBounds.x, relativePosition.y * DemiBounds.y);
    }

    public static Vector3 ComputePositionClamped(Vector2 relativePosition)
    {
        return new Vector3(Mathf.Clamp(relativePosition.x * DemiBounds.x, -DemiBounds.x, DemiBounds.x),
            Mathf.Clamp(relativePosition.y * DemiBounds.y, -DemiBounds.y, DemiBounds.y), 0);
    }

    public static Vector3 ComputePosition(float relativeX, float relativeY)
    {
        return new Vector3(relativeX * DemiBounds.x, relativeY * DemiBounds.y, 0);
    }

    public static Vector3 ComputePositionFromScreenPosition(Vector2 screenPosition)
    {
        return ComputeNormalizedPosition(new Vector2(screenPosition.x / Resolution.x, screenPosition.y / Resolution.y));
    }

    public static Vector2 ComputeRelativeDeltaFromScreenDelta(Vector2 screenDelta)
    {
        var deltaX = screenDelta.x / Resolution.x;
        var deltaY = screenDelta.y / Resolution.y;
        return new Vector2(deltaX * 2, deltaY * 2);
    }

    public static Vector3 ComputeNormalizedPosition(Vector2 normalizedPosition)
    {
        return new Vector3(normalizedPosition.x * ScreenBounds.x - DemiBounds.x,
            normalizedPosition.y * ScreenBounds.y - DemiBounds.y, 0);
    }

    public static Vector2 GetReferenceRelativePosition(Vector3 basePos)
    {
        var absX = Mathf.Abs(basePos.x);
        var xNeg = basePos.x < 0;
        var absY = Mathf.Abs(basePos.y);
        var yNeg = basePos.y < 0;
        var x = absX / RefDemiBounds.x * (xNeg ? -1f : 1f);
        var y = absY / RefDemiBounds.y * (yNeg ? -1f : 1f);

        return new Vector2(x, y);
    }

    public static Vector3 ComputeCorrectPosition(Vector3 basePosition)
    {
        return RatioDiff == 1 ? basePosition : ComputePosition(GetReferenceRelativePosition(basePosition));
    }

    #endregion

    public static void MoveRigidBodyOptimized(Rigidbody2D rigidbody2D, Vector2 relativeDelta)
    {
        if (!precomputedVectors.TryGetValue(relativeDelta, out var computedVector))
        {
            computedVector = ComputeVector2Position(relativeDelta);
            precomputedVectors[relativeDelta] = computedVector;
        }

        rigidbody2D.MovePosition(rigidbody2D.position + computedVector);
    }

    public void OnEnable()
    {
        GameManager.OnGameOver += ClearComputeCache;
        Optimization.OnBeforeGCCollect += ClearComputeCache;
    }

    public void OnDisable()
    {
        GameManager.OnGameOver -= ClearComputeCache;
        Optimization.OnBeforeGCCollect -= ClearComputeCache;
    }

    private static void ClearComputeCache()
    {
        precomputedVectors.Clear();
    }
}