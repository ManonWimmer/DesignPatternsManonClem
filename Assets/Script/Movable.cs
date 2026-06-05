using System;
using UnityEngine;

public class Movable : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rb;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected Animator _animator;

    protected float _currentSpeed;


    public Rigidbody Rb => _rb;
    public float MoveSpeed => _moveSpeed;
    public float CurrentSpeed => _currentSpeed;
    public Animator Animator => _animator;

    public event Action<Vector3, Movable> StartMove;
    public event Action Undo;

    private void Start()
    {
        StartMove?.Invoke(transform.position, this);
    }

    public virtual void Move(Vector3 direction)
    {
        float speedByTime = _moveSpeed * Time.deltaTime;
        Vector3 targetPosition = new Vector3(transform.position.x + direction.x * speedByTime, transform.position.y, transform.position.z + direction.y * speedByTime);
        StartMove?.Invoke(targetPosition, this);
    }

    public virtual void StartUndo()
    {
        Undo?.Invoke();
    }

    public void ResetAnimation()
    {
        _animator.SetFloat("WalkSpeed", _currentSpeed);
    }
}
