using UnityEngine;

public class SimpleGameEventListener : GameEventListener<Void>
{
    [SerializeField] SimpleGameEvent simpleEvent;

    protected override GameEvent<Void> Event => simpleEvent;
}
