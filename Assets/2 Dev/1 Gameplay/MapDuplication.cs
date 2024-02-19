using UnityEngine;

public class MapDuplication : MonoBehaviour
{
    public Transform player;
    public GameObject mapPrefab;

    [SerializeField] float speed = 2f;
    public float mapHeight = 20f;

    private GameObject[] maps = new GameObject[2];

    void Start()
    {
        Vector3 secondMapPosition = new Vector3(player.position.x, player.position.y + mapHeight, player.position.z);

        // create the maps
        maps[0] = Instantiate(mapPrefab, player.position, Quaternion.identity);
        maps[1] = Instantiate(mapPrefab, secondMapPosition, Quaternion.identity);
    }

    void Update()
    {
        // Check if the maps are too low compared to the player
        foreach (GameObject map in maps)
        {
            if (map.transform.position.y < player.position.y - mapHeight)
            {
                map.transform.position = new Vector3(map.transform.position.x, player.position.y + mapHeight, map.transform.position.z);
            }
        }
    }

    void LateUpdate()
    {
        // Move both maps downwards
        foreach (GameObject map in maps)
        {
            map.transform.position += Vector3.down * (speed * Time.deltaTime);
        }
    }
}
