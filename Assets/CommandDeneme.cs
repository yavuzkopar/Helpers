using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CommandDeneme<T>
{
  //  List<Action<T>> actions = new List<Action<T>>();
    int index = 0;
    Dictionary<Action,Action> keyValuePairs = new Dictionary<Action,Action>();
   // List<Func<T>> func = new List<Func<T>>();
    delegate void delegates(T param);
    List<delegates> collection = new List<delegates>();
    public delegate void ActionToUse(T param);
    ActionToUse actionToUse;
    public List<ActionToUse> actions = new List<ActionToUse>();

    //public void Add(Action<T> action)
    //{
    //    actions.Add(action);
    //}
    //public void Remove(Action<T> action)
    //{
    //    actions.Remove(action);
    //}
    public void AddWithUndo(Action action,Action undo)
    {
        keyValuePairs.Add(action, undo);
    }
    public void ExecuteWithUndo(Action action)
    {
        keyValuePairs[action]?.Invoke();
    }
    //public void Execute()
    //{
    //    if (index < actions.Count)
    //    {
    //       var a = actions[index];
    //        a.DynamicInvoke();
    //        index++;
    //    }
    //}
    public void Execute(ActionToUse action,T param)
    {
        
        actions.Add(action);
        if (index < actions.Count)
        {

            actions[index].Invoke(param);
            index++;
        }
    }
    //public void Execute(delegate action<T>)
    //{
    //    collection.Add(action);
    //    if (index < collection.Count)
    //    {

    //        collection[index]?.Invoke(T);
    //        index++;
    //    }
    //}
    public void Undo(T param)
    {
        
        if (index > 0)
        {
            actions[actions.Count - 1]?.Invoke(param);
            actions.RemoveAt(actions.Count - 1);
            index--;
        }
    }
    //public void Undo()
    //{
    //    if (index > 0)
    //    {
    //        func[func.Count - 1]?.Invoke();
    //        func.RemoveAt(func.Count - 1);
    //        index--;
    //    }
    //}
}
