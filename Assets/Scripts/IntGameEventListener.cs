using UnityEngine;

public class IntGameEventListener : GameEventListener<int>
{
    [SerializeField] IntGameEvent intEvent;

    protected override GameEvent<int> Event => intEvent;
}
