using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Invoker
{
    private Stack<Command> _commands;
    public int CommandsCount => _commands.Count;

    public Invoker()
    {
        _commands = new Stack<Command>();
    }  

    public void Execute(Command command)
    {
        if (command == null) return;

        _commands.Push(command);
        command.Execute();
    }

    public void Undo()
    {
        if(_commands.Count <= 0 )return;

        _commands.Pop().Undo();
    }
}
