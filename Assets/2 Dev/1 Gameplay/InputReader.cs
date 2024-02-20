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
        playerInput.actions["Touch"].canceled += OnCanceledTouch;
    }
    private void OnDisable()
    {
        playerInput.actions["Move"].performed -= OnInputMove;
        playerInput.actions["Touch"].canceled -= OnCanceledTouch;
    }

    bool _startTouch = true;
    private void OnCanceledTouch(InputAction.CallbackContext context)
    {
        _startTouch = true;
    }

    private Vector2 _previousMousePos;
    private void OnInputMove(InputAction.CallbackContext context)
    {
        Vector2 current = context.ReadValue<Vector2>();
        Vector2 delta = current - _previousMousePos;

        if (!_startTouch)
            OnMove?.Invoke(delta);

        _startTouch = false;
        _previousMousePos = current;
    }
}