using UnityEngine;

public abstract class Command
{
    protected readonly float _timeStamp;

    public Command()
    {
        _timeStamp = Time.time;
    }

    public abstract void Execute();
    public abstract void Undo();
}
