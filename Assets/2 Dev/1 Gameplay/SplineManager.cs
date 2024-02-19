using UnityEngine;
using UnityEngine.Splines;

public enum EnemyPath
{
    FIRST,
}

public class SplineManager : MonoBehaviour
{
    #region Singleton

    private static SplineManager I { get; set; }
    private void Awake()
    {
        if (I != null)
        {
            Destroy(gameObject);
            return;
        }
        I = this;
    }

    #endregion

    public static SplineContainer GetSplineContainer(EnemyPath path)
    {
        Debug.Log("TODO: enum values");
        return null;
    }
}
