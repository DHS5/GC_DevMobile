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

    [Space(5f)]

    [SerializeField] private SpriteRenderer _spriteRenderer;

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
    }
    private void OnDisable()
    {
        inputReader.OnMove -= OnMove;
    }

    private void OnMove(Vector2 screenPosition)
    {
        // todo check collision when 
        _spriteRenderer.SetPositionFromScreenPosition(screenPosition);
    }
}