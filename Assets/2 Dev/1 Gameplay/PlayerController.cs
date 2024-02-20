using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;

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
        transform.MovePlayerClamp(Format.ComputeRelativeDeltaFromScreenDelta(screenDelta));
    }
}