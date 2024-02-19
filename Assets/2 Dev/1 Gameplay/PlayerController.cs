using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Sprite sprite;
    [SerializeField] private float relativeSize = 1;
    [SerializeField] private Vector2 relativeStartPosition = new Vector2(0, -0.5f);

    [Header("References")]
    [SerializeField] private InputReader inputReader;


    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = Pool.GetSpriteRenderer();
        _spriteRenderer.sprite = sprite;
        _spriteRenderer.SetRelativeSize(relativeSize, 1);
        _spriteRenderer.SetRelativePosition(relativeStartPosition);
    }

    private void OnEnable()
    {
        inputReader.OnMove += OnMove;
        inputReader.OnFire += OnFire;
    }
    private void OnDisable()
    {
        inputReader.OnMove -= OnMove;
        inputReader.OnFire -= OnFire;
    }

    private void OnMove(Vector2 screenPosition)
    {
        _spriteRenderer.SetPositionFromScreenPosition(screenPosition);
    }

    private void OnFire()
    {

    }

    //void Update()
    //{
    //    Touch touch = Input.GetTouch(0);
    //
    //    targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
    //
    //    spriteRenderer.Move(targetPosition);
    //
    //
    //    // DEBUG - to play on pc
    //    // ---------------------
    //    targetPosition += new Vector3(input.Move.x, input.Move.y, 0f) * (speed * Time.deltaTime);
    //
    //    // Calculate the min and max X and Y positions for the player based on the camera view
    //    var minPlayerX = cameraFollow.position.x + minX;
    //    var maxPlayerX = cameraFollow.position.x + maxX;
    //    var minPlayerY = cameraFollow.position.y + minY;
    //    var maxPlayerY = cameraFollow.position.y + maxY;
    //
    //    // Clamp the player's position to the camera view
    //    targetPosition.x = Mathf.Clamp(targetPosition.x, minPlayerX, maxPlayerX);
    //    targetPosition.y = Mathf.Clamp(targetPosition.y, minPlayerY, maxPlayerY);
    //
    //    // Lerp the player's position to the target position
    //    transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothness);
    //
    //    // Calculate the rotation effect
    //    var targetRotationAngle = -input.Move.x * leanAngle;
    //
    //    var currentYRotation = transform.localEulerAngles.y;
    //    var newYRotation = Mathf.LerpAngle(currentYRotation, targetRotationAngle, leanSpeed * Time.deltaTime);
    //
    //    // Apply the rotation effect
    //    transform.localEulerAngles = new Vector3(0f, newYRotation, 0f);
    //    // ---------------------
    //}
}