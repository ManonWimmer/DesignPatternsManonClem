using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    // ----- FIELDS ----- //
    [Header("Input")]
    [SerializeField] private InputActionReference _attackInput;

    [Header("Attack")]
    [SerializeField] private AttackController _attackController;

    public event Action OnAttackPressed;
    // ----- FIELDS ----- //

    private void Start()
    {
        if (_attackInput)
            _attackInput.action.started += ReceiveAttackInput;

        if (_attackController)
            OnAttackPressed += _attackController.Attack;
    }

    private void OnDestroy()
    {
        if (_attackInput)
            _attackInput.action.started -= ReceiveAttackInput;

        if (_attackController)
            OnAttackPressed -= _attackController.Attack;
    }

    private void ReceiveAttackInput(InputAction.CallbackContext obj)
    {
        print("attack pressed");
        OnAttackPressed?.Invoke();
    }
}
