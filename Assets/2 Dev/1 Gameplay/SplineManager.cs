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

    private void Start()
    {
        ResizeSplines();
    }

    public void ResizeSplines()
    {
        BezierKnot[] knots;
        BezierKnot knot;
        foreach (var spline in splinePaths)
        {
            knots = spline.Spline.ToArray();
            for (int i = 0; i < knots.Length; i++)
            {
                knot = knots[i];
                Debug.Log("Base pos " + knot.Position);
                knot.Position = Format.ComputeCorrectPosition(knot.Position);
                Debug.Log("New pos " + knot.Position);
                spline.Spline.SetKnot(i, knot);
            }
        }
    }
}
