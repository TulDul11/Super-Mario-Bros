using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decision/Timer")]
public class TimerDecision : Decision
{
    public float duration = 2f;
    
    public override bool Decide(StateController controller) => controller.CheckIfCountDownElasped(duration);

}
