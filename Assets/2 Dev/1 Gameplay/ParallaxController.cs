using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform[] backgrounds; // Array of background layers
    [SerializeField] private float smoothing = 10f; // How smooth the parallax effect is
    [SerializeField] private float multiplier = 15f; // How much the parallax effect increments per layer

    private Transform cam; // Reference to the main camera
    private Vector3 previousCamPos; // Position of the camera in the previous frame

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    private void Start()
    {
        previousCamPos = cam.position;
    }

    private void Update()
    {
        // Iterate through each background layer
        for (var i = 0; i < backgrounds.Length; i++)
        {
            var parallax = (previousCamPos.y - cam.position.y) * (i * multiplier);
            var targetY = backgrounds[i].position.y + parallax;

            var targetPosition = new Vector3(backgrounds[i].position.x, targetY, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, targetPosition, smoothing * Time.deltaTime);
        }

        previousCamPos = cam.position;
    }
}