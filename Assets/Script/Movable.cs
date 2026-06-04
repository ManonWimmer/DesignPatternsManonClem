using System;
using UnityEngine;

public class Movable : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rb;
    [SerializeField] protected float _moveSpeed;

    public Rigidbody Rb => _rb;

    public event Action<Vector3> StartMove;
    public event Action StopMove;
    public event Action Undo;

    private void Start()
    {
        StartMove?.Invoke(transform.position);
    }

    public virtual void Move(Vector3 direction)
    {
        float speedByTime = _moveSpeed * Time.deltaTime;
        Vector3 targetPosition = new Vector3(transform.position.x + direction.x * speedByTime, transform.position.y, transform.position.z + direction.y * speedByTime);
        StartMove?.Invoke(targetPosition);
    }

    public virtual void CancelMove()
    {
        //StopMove?.Invoke();
    }

    public virtual void StartUndo()
    {
        Undo?.Invoke();
    }
}
