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

    #region Global Members

    [SerializeField] private EnumValues<EnemyPath, SplineContainer> splinePaths;

    #endregion

    public static SplineContainer GetSplineContainer(EnemyPath path)
    {
        return I.splinePaths.Get(path);
    }

    public void ResizeSplines()
    {
        //foreach (var spline in splinePaths)
        //{
        //
        //}
    }
}
