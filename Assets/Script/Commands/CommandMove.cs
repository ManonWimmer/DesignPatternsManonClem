using UnityEngine;

public class CommandMove : Command
{
    private Rigidbody _rb;
    private Vector3 _position;
    private Quaternion _rotation;

    public CommandMove(Rigidbody rb, Vector3 position, Quaternion rotation)
    {
        _rb = rb;
        _position = position;
        _rotation = rotation;
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
