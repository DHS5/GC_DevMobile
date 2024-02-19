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
    public static Vector2 DemiBounds { get; private set; }

    #endregion

    #region Initialization

    private void Init()
    {
        Resolution = new Vector2(Screen.width, Screen.height);
        
        ReferenceRatio = (float)referenceResolution.x / referenceResolution.y;
        Ratio = Resolution.x / Resolution.y;
        RatioDiff = Ratio / ReferenceRatio;

        ResolutionDelta = new Vector2(Resolution.x / referenceResolution.x, Resolution.y / referenceResolution.y);
        ScaleFactor = Mathf.Min(ResolutionDelta.x, ResolutionDelta.y);

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

    #endregion
}
