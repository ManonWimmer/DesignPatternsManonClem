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

    [Header("Check is dead")]
    [SerializeField] private HealthController _healthController;

    private bool _isMoving = false;
    private bool _isDead = false;

    private Vector3 _direction;

    public event Action<Vector3> DirectionValueChanged;

    private void Awake()
    {
        _moveInput.action.performed += ReceiveStartInput;
        _moveInput.action.canceled += ReceiveStopInput;

        _undoInput.action.performed += ReceiveUndo;

        if (_healthController)
            _healthController.OnDie += HandleDeath;
    }

    private void OnDestroy()
    {
        _moveInput.action.performed -= ReceiveStartInput;
        _moveInput.action.canceled -= ReceiveStopInput;

        _undoInput.action.performed -= ReceiveUndo;

        if (_healthController)
            _healthController.OnDie -= HandleDeath;
    }

    private void HandleDeath()
    {
        _isDead = true;
    }

    private void FixedUpdate()
    {
        if (_isDead)
            return;

        if (_isMoving)
        {
            Move(_direction);
            float angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;
            _gameObjectToMove.transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    private void ReceiveStartInput(InputAction.CallbackContext obj)
    {
        if (_isDead)
            return;

        print("receive input");
        _isMoving = true;

        _direction = obj.ReadValue<Vector2>();

        _currentSpeed = _moveSpeed;
    }

    private void ReceiveStopInput(InputAction.CallbackContext obj)
    {
        if (_isDead)
            return;

        print("stop input");
        _isMoving = false;

        _currentSpeed = 0;
        ResetAnimation();
    }

    private void ReceiveUndo(InputAction.CallbackContext obj)
    {
        if (_isDead)
            return;

        StartUndo();
    }
}
