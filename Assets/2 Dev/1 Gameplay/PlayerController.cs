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
}