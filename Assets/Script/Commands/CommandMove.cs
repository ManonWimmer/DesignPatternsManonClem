using UnityEngine;

public class CommandMove : Command
{
    private Rigidbody _rb;
    private Vector3 _position;
    private Movable _movable;

    private Quaternion _rotation;
    private float _currentSpeed;

    public CommandMove(Rigidbody rb, Vector3 position, Movable movable)
    {
        _rb = rb;
        _position = position;
        _movable = movable;

        _rotation = movable.transform.rotation;
        _currentSpeed = movable.CurrentSpeed;
    }

    public override void Execute()
    {
        _rb.MovePosition(_position);
        _rb.MoveRotation(_rotation);
    }

    public override void Undo()
    {
        _rb.MovePosition(_position);
        _rb.MoveRotation(_rotation);
    }
}
