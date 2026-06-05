using System.Collections;
using UnityEngine;

public class MoveCommandHandler : MonoBehaviour
{
    [SerializeField] private Movable _moveable;

    private Invoker _invoker;
    private Coroutine _undoCoroutine;

    private void Awake()
    {
        _invoker = new Invoker();

        _moveable.StartMove += HandleStartMove;

        _moveable.Undo += HandleUndo;
    }

    private void OnDestroy()
    {
        _moveable.StartMove -= HandleStartMove;
    }

    private void HandleStartMove(Vector3 position)
    {
        Command move = new CommandMove(_moveable.Rb, position);
        _invoker.Execute(move);
    }

    private void HandleStopMove()
    {
        CommandMove move = new CommandMove(_moveable.Rb, Vector3.zero);
        _invoker.Execute(move);
    }

    private void HandleUndo()
    {
        if (_undoCoroutine == null)
            _undoCoroutine = StartCoroutine(UndoCoroutine());
        else
        {
            Debug.Log("coroutine not null");
        }
    }

    private IEnumerator UndoCoroutine()
    {
        while(_invoker.CommandsCount > 0)
        {
            _invoker.Undo();
            yield return null;
        }

        _undoCoroutine = null;
        Debug.Log("test");
    }

    private void Update()
    {
        //Debug.Log(_invoker.CommandsCount);
    }
}
