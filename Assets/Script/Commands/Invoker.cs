using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Invoker
{
    int _maxCommandsCount;

    private CustomStack<Command> _commands;
    public int CommandsCount => _commands.Count;

    public Invoker(int maxCommands)
    {
        _commands = new CustomStack<Command>();
        _maxCommandsCount = maxCommands;
    }  

    public void Execute(Command command)
    {
        if (command == null) return;

        _commands.Push(command);
        command.Execute();
        if(_commands.Count > _maxCommandsCount)
        {
            _commands.RemoveAt(0);
        }
    }

    public void Undo()
    {
        if(_commands.Count <= 0 )return;

        _commands.Pop().Undo();
    }
}
