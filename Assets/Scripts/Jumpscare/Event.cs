using System.Collections.Generic;

public class Event : IEvent
{
    private readonly List<IAction> actions = new List<IAction>();
    public void AddListener(IAction action)
    {
        if(!actions.Contains(action))
        {
            actions.Add(action);
        }
    }

    public void Notify()
    {
        foreach (var action in actions)
        {
            action.Execute();
        }
    }

    public void RemoveListener(IAction action)
    {
        if(actions.Contains(action))
        {
            actions.Remove(action);
        }
    }
}