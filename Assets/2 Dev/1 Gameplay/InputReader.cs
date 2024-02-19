using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputReader : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInput playerInput;

    public event Action<Vector2> OnMove;
    public event Action OnFire;

    private void OnEnable()
    {
        playerInput.actions["Move"].performed += OnInputMove;
        playerInput.actions["Fire"].performed += OnInputFire;
    }
    private void OnDisable()
    {
        playerInput.actions["Move"].performed -= OnInputMove;
        playerInput.actions["Fire"].performed -= OnInputFire;
    }

    private void OnInputMove(InputAction.CallbackContext context)
    {
        OnMove?.Invoke(context.ReadValue<Vector2>());
    }
    private void OnInputFire(InputAction.CallbackContext context)
    {
        OnFire?.Invoke();
    }
}