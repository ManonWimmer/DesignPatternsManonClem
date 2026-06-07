using System;
using UnityEngine;

public class Movable : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rb;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected GameObject _gameObjectToMove;


    protected float _currentSpeed;


    public Rigidbody Rb => _rb;
    public float MoveSpeed => _moveSpeed;
    public float CurrentSpeed => _currentSpeed;

    public event Action<Vector3, Movable> StartMove;
    public event Action Undo;

    public event Action<float> OnSpeedChanged;
    public event Action<float, float> OnMoveAnimationRequested;

    private void Start()
    {
        StartMove?.Invoke(_gameObjectToMove.transform.position, this);
    }

    public virtual void Move(Vector3 direction)
    {
        float speedByTime = _moveSpeed * Time.deltaTime;
        Vector3 targetPosition = new Vector3(_gameObjectToMove.transform.position.x + direction.x * speedByTime, _gameObjectToMove.transform.position.y, _gameObjectToMove.transform.position.z + direction.y * speedByTime);
        StartMove?.Invoke(targetPosition, this);

        OnSpeedChanged?.Invoke(_currentSpeed);
    }

    public virtual void StartUndo()
    {
        Undo?.Invoke();
    }

    public void ResetAnimation()
    {
        OnSpeedChanged?.Invoke(_currentSpeed);
    }

    public void PlayMoveAnimation(float speed, float reverse)
    {
        OnMoveAnimationRequested?.Invoke(speed, reverse);
    }
}
