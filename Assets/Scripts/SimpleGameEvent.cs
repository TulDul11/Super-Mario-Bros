using UnityEngine;

[CreateAssetMenu(fileName = "SimpleGameEvent", menuName = "ScriptableObjects/SimpleGameEvent", order = 4)]
public class SimpleGameEvent : GameEvent<Void>
{
    public void Raise() => Raise(new Void());
}