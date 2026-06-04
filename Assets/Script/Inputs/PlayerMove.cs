using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMove : Movable
{
    [SerializeField] private InputActionReference _moveInput;
    [SerializeField] private InputActionReference _undoInput;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _moveInput.action.performed += ReceiveStartInput;
        _moveInput.action.canceled += ReceiveStopInput;

        _undoInput.action.performed += ReceiveUndo;
    }

    private void OnDestroy()
    {
        _moveInput.action.performed -= ReceiveStartInput;
        _moveInput.action.canceled -= ReceiveStopInput;

        _undoInput.action.performed -= ReceiveUndo;
    }

    private void ReceiveStartInput(InputAction.CallbackContext obj)
    {
        Vector2 direction = obj.ReadValue<Vector2>();

        Move(direction);

        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);
        _animator.SetFloat("WalkSpeed", _moveSpeed * Time.deltaTime);

    }

    private void ReceiveStopInput(InputAction.CallbackContext obj)
    {
        CancelMove();
        _animator.SetFloat("WalkSpeed", 0);
    }

    private void ReceiveUndo(InputAction.CallbackContext obj)
    {
        StartUndo();
    }
}
