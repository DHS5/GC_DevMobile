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

    public static Resolution Resolution => Screen.currentResolution;


    public static float Ratio { get; private set; }
    public static float ReferenceRatio { get; private set; }
    
    public static Vector2 ResolutionDelta { get; private set; }
    public static float ScaleFactor { get; private set; }

    public static Vector2 ScreenBounds { get; private set; }
    public static Vector2 DemiBounds { get; private set; }

    #endregion

    #region Initialization

    private void Init()
    {
        ReferenceRatio = referenceResolution.x / referenceResolution.y;
        Ratio = Resolution.width / Resolution.height;

        ResolutionDelta = new Vector2(Resolution.width / referenceResolution.x, Resolution.height / referenceResolution.y);
        ScaleFactor = Mathf.Min(ResolutionDelta.x, ResolutionDelta.y);

        ScreenBounds = new Vector2(referenceBounds.x * ResolutionDelta.x, referenceBounds.y * ResolutionDelta.y);
        DemiBounds = new Vector2(ScreenBounds.x / 2, ScreenBounds.y / 2);
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

    public static Vector2 ComputePosition(Vector2 relativePosition)
    {
        return new Vector2(relativePosition.x * DemiBounds.x, relativePosition.y * DemiBounds.y);
    }

    #endregion
}
