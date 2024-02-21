using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputReader : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private PlayerInput playerInput;

    public event Action<Vector2> OnMove;

    private void OnEnable()
    {
        playerInput.actions["Move"].performed += OnInputMove;
        playerInput.actions["Touch"].canceled += OnCanceledTouch;

        PauseMenu.OnGamePause += OnGamePaused;
        PauseMenu.OnGameResume += OnGameResumed;
    }

    private void OnDisable()
    {
        playerInput.actions["Move"].performed -= OnInputMove;
        playerInput.actions["Touch"].canceled -= OnCanceledTouch;

        PauseMenu.OnGamePause -= OnGamePaused;
        PauseMenu.OnGameResume -= OnGameResumed;
    }

    private bool _startTouch = true;

    private void OnCanceledTouch(InputAction.CallbackContext context)
    {
        _startTouch = true;
    }

    private Vector2 _previousMousePos;

    private void OnInputMove(InputAction.CallbackContext context)
    {
        var current = context.ReadValue<Vector2>();
        var delta = current - _previousMousePos;

        if (!_startTouch)
            OnMove?.Invoke(delta);

        _startTouch = false;
        _previousMousePos = current;
    }

    private void OnGamePaused()
    {
        playerInput.actions["Move"].performed -= OnInputMove;
        playerInput.actions["Touch"].canceled -= OnCanceledTouch;
    }

    private void OnGameResumed()
    {
        playerInput.actions["Move"].performed += OnInputMove;
        playerInput.actions["Touch"].canceled += OnCanceledTouch;

        _startTouch = true;
    }
}