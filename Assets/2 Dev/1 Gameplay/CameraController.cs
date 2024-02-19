using UnityEngine;

    public class CameraController : MonoBehaviour
    {
        [SerializeField] Transform player;

        void Start() => transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }