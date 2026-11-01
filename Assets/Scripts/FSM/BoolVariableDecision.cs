using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decision/BoolVariable")]
public class BoolVariableDecision : Decision
{
    public BoolVariable variable;
    
    public override bool Decide(StateController controller) => variable.Value;

}
