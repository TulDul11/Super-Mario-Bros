using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Blink")]
public class BlinkAction : StateAction
{
    public bool blink;
    public override void Act(StateController controller) => controller.animator.SetInvincibleFlicker(blink);
}