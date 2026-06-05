using System;
using UnityEngine;

public class Movable : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rb;
    [SerializeField] protected float _moveSpeed;

    public Rigidbody Rb => _rb;

    public event Action<Vector3, Quaternion> StartMove;
    public event Action Undo;

    private void Start()
    {
        StartMove?.Invoke(transform.position, transform.rotation);
    }

    public virtual void Move(Vector3 direction, Quaternion rotation)
    {
        float speedByTime = _moveSpeed * Time.deltaTime;
        Vector3 targetPosition = new Vector3(transform.position.x + direction.x * speedByTime, transform.position.y, transform.position.z + direction.y * speedByTime);
        StartMove?.Invoke(targetPosition, rotation);
    }

    public virtual void StartUndo()
    {
        Undo?.Invoke();
    }
}
