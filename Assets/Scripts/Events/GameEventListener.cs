using UnityEngine;
using UnityEngine.Events;

public abstract class GameEventListener<T> : MonoBehaviour
{
    public UnityEvent<T> response;
    protected abstract GameEvent<T> Event { get; }

    void OnEnable()
    {
        if (Event != null)
        {
            Event.RegisterListener(this);
        }
    }

    void OnDisable()
    {
        if (Event != null)
        {
            Event.UnregisterListener(this);
        }
    }

    public void OnEventRaised(T data)
    {
        response.Invoke(data);
    }
}
