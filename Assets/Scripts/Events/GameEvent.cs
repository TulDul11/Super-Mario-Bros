using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent<T> : ScriptableObject
{
    readonly List<GameEventListener<T>> listeners = new List<GameEventListener<T>>();

    public void Raise(T data)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(data);
        }
    }

    public void RegisterListener(GameEventListener<T> l)
    {
        if (!listeners.Contains(l))
        {
            listeners.Add(l);
        }
    }

    public void UnregisterListener(GameEventListener<T> l)
    {
        if (listeners.Contains(l))
        {
            listeners.Remove(l);
        }
    }
}