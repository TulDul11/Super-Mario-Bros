using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Death")]
public class DeathAction : StateAction
{
    public override void Act(StateController controller) => controller.player.Die();
}