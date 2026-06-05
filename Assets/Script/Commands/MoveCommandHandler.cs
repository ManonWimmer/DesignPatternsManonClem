using System.Collections;
using UnityEngine;

public class MoveCommandHandler : MonoBehaviour
{
    [SerializeField] private Movable _moveable;

    [SerializeField] private Invoker _invoker;
    [SerializeField] private int _maxCommandsCount;
    private Coroutine _undoCoroutine;

    private void Awake()
    {
        _invoker = new Invoker(_maxCommandsCount);

        _moveable.StartMove += HandleStartMove;

        _moveable.Undo += HandleUndo;
    }

    private void OnDestroy()
    {
        _moveable.StartMove -= HandleStartMove;
    }

    private void HandleStartMove(Vector3 position, Movable movable)
    {
        Command move = new CommandMove(_moveable.Rb, position, movable);
        _invoker.Execute(move);
    }

    private void HandleUndo()
    {
        if (_undoCoroutine == null)
            _undoCoroutine = StartCoroutine(UndoCoroutine());
    }

    private IEnumerator UndoCoroutine()
    {
        while(_invoker.CommandsCount > 0)
        {
            _invoker.Undo();
            yield return null;
        }
        _moveable.ResetAnimation();
        _undoCoroutine = null;
    }
}
