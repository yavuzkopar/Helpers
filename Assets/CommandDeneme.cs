using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CommandDeneme
{
    public List<IAction> actioners = new List<IAction>();
    int lastActionIndex = -1;

    public void Execute(IAction action)
    {
        if (lastActionIndex + 1 >= actioners.Count)
            actioners.Add(action);
        lastActionIndex++;
        actioners[lastActionIndex].Execute();
    }
    public void Undo()
    {
        if (lastActionIndex < 0) return;
        actioners[lastActionIndex].Undo();
        lastActionIndex--;
    }

}
public interface IAction
{
    void Execute();
    void Undo();
}
