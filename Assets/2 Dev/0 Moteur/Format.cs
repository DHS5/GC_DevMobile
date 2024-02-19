using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Format : MonoBehaviour
{
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

    [SerializeField] private Vector2Int referenceResolution = new Vector2Int(1920, 1080);

    #endregion

    #region Static Members

    public static Resolution Resolution => Screen.currentResolution;


    public static float Ratio { get; private set; }

    public static float ReferenceRatio { get; private set; }
    public static float ScaleFactor { get; private set; }

    public static Vector2 ResolutionDelta { get; private set; }

    #endregion

    #region Initialization

    private void Init()
    {
        ReferenceRatio = referenceResolution.x / referenceResolution.y;
        Ratio = Resolution.width / Resolution.height;

        ResolutionDelta = new Vector2(Resolution.width / referenceResolution.x, Resolution.height / referenceResolution.y);
        ScaleFactor = Mathf.Min(ResolutionDelta.x, ResolutionDelta.y);
    }

    #endregion

    #region Size

    public static Vector2 ComputeSize(float relativeSize, float ratio)
    {
        float factor = relativeSize * ScaleFactor;
        return new Vector2(ratio * factor, factor);
    }

    #endregion
}
