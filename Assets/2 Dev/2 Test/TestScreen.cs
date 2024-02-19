using UnityEngine;

namespace _2_Dev._2_Test
{
    public class TestScreen : MonoBehaviour
    {
        
        [SerializeField] private SpriteRenderer TL_Point;
        [SerializeField] private SpriteRenderer TR_Point;
        [SerializeField] private SpriteRenderer Center_Point;
        [SerializeField] private SpriteRenderer BL_Point;
        [SerializeField] private SpriteRenderer BR_Point;
        
        private void Start()
        {            
            TL_Point.SetRelativePosition(new Vector2(-1, 1));
            TR_Point.SetRelativePosition(new Vector2(1, 1));
            BL_Point.SetRelativePosition(new Vector2(-1, -1));
            BR_Point.SetRelativePosition(new Vector2(1, -1));
            
            Center_Point.SetRelativePosition(Vector2.zero);
        }
    }
}