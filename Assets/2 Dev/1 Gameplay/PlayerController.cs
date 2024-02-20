using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Sprite sprite;
    [SerializeField] private float relativeSize = 1;
    [SerializeField] private Vector2 relativeStartPosition = new Vector2(0, -0.5f);

    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Weapon weapon;
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {
        //_spriteRenderer = Pool.GetSpriteRenderer();
        spriteRenderer.sprite = sprite;
        spriteRenderer.transform.SetRelativeSize(relativeSize, 1);
        spriteRenderer.transform.SetRelativePosition(relativeStartPosition);
    }

    private void OnEnable()
    {
        inputReader.OnMove += OnMove;
    }
    private void OnDisable()
    {
        inputReader.OnMove -= OnMove;
    }

    private void OnMove(Vector2 screenDelta)
    {
        //Debug.Log(screenDelta + " " + UnityEngine.InputSystem.Touchscreen.)
        spriteRenderer.transform.Move(Format.ComputeDeltaFromScreenDelta(screenDelta));
    }
}