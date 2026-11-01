using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/SetBoolVariable")]
public class SetBoolVariableAction : StateAction
{
    public BoolVariable target;
    public bool value;
    public override void Act(StateController controller) => target.Value = value;
}
