using UnityEngine;

public class CommandMove : Command
{
    private Rigidbody _rb;
    private Vector3 _position;

    public CommandMove(Rigidbody rb, Vector3 position)
    {
        _rb = rb;
        _position = position;
    }

    public override void Execute()
    {
        _rb.MovePosition(_position);
    }

    public override void Undo()
    {
        _rb.MovePosition(_position);
    }
}
