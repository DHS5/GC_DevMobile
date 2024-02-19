using UnityEngine;
using UnityEngine.Serialization;

namespace _2_Dev._2_Test
{
    public class TestScreen : MonoBehaviour
    {
        
        [SerializeField] private SpriteRenderer TL_Point;
        [SerializeField] private SpriteRenderer TR_Point;
        [FormerlySerializedAs("Center_Point")] [SerializeField] private SpriteRenderer centerPoint;
        [SerializeField] private SpriteRenderer BL_Point;
        [SerializeField] private SpriteRenderer BR_Point;
        
        [FormerlySerializedAs("PointList")] [SerializeField] private SpriteRenderer[] pointList;
        
        private void Start()
        {            
            TL_Point.SetRelativePosition(new Vector2(-1, 1));
            TR_Point.SetRelativePosition(new Vector2(1, 1)); 
            BL_Point.SetRelativePosition(new Vector2(-1, -1));
            BR_Point.SetRelativePosition(new Vector2(1, -1));
            
            centerPoint.SetRelativePosition(Vector2.zero);
            
            pointList = new[] {TL_Point, TR_Point, BL_Point, BR_Point, centerPoint};
            
            foreach (var point in pointList)
            {
                point.SetRelativeSize(1f, 1);
            }
        }
    }
}