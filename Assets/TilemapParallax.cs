using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapParallax : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 1.0f;
    [SerializeField] GameObject viewTarget;

    [SerializeField] Tilemap tileMap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ParallaxMove()
    {
        float newXPos = viewTarget.transform.position.x * scrollSpeed;
        float newYPos = viewTarget.transform.position.y * scrollSpeed;

        tileMap.transform.position = new Vector3(newXPos, newYPos, tileMap.transform.position.z);
    }
}
