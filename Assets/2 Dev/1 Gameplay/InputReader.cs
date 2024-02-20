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


    private Vector2 _previousMousePos;
    private void OnInputMove(InputAction.CallbackContext context)
    {
        Vector2 current = context.ReadValue<Vector2>();
        Vector2 delta = current - _previousMousePos;
        Debug.Log(delta + " " + delta.magnitude);
        if (delta.magnitude < 50)
            OnMove?.Invoke(delta);
        _previousMousePos = current;
    }
    private void OnInputFire(InputAction.CallbackContext context)
    {
        OnFire?.Invoke();
    }
}