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

    private static float _ratio = 0f;
    public static float Ratio
    {
        get
        {
            if (_ratio == 0f) _ratio = Resolution.width / Resolution.height;
            return _ratio;
        }
    }



    #endregion

    #region Initialization

    private void Init()
    {

    }

    #endregion

    #region Size

    public static Vector2 ComputeSize(float relativeSize, float ratio)
    {
        return Vector2.one;
    }

    #endregion
}
