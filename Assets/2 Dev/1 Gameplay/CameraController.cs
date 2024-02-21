using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Start()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}