using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/SetForm")]
public class SetFormAction : StateAction
{
    public bool super;
    public override void Act(StateController controller)
    {
        if (super)
        {
            controller.player.MakeSuper();
        }
        else
        {
            controller.player.Shrink();
        }
    }
}
