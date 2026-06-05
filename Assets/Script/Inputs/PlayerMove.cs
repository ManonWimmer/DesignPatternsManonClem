using CartoonFX;
using System;
using Unity.AppUI.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMove : Movable
{
    [SerializeField] private InputActionReference _moveInput;
    [SerializeField] private InputActionReference _undoInput;

    private bool _isMoving = false;

    private Vector3 _direction;

    public event Action<Vector3> DirectionValueChanged;

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

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            Move(_direction);
            float angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    private void ReceiveStartInput(InputAction.CallbackContext obj)
    {
        _isMoving = true;

        _direction = obj.ReadValue<Vector2>();

        _currentSpeed = _moveSpeed;
    }

    private void ReceiveStopInput(InputAction.CallbackContext obj)
    {
        _isMoving = false;

        _animator.SetFloat("WalkSpeed", 0);

        _currentSpeed = 0;
    }

    private void ReceiveUndo(InputAction.CallbackContext obj)
    {
        StartUndo();
    }
}
