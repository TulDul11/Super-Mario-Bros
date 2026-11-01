using UnityEngine;

[CreateAssetMenu(menuName = "FSM/State")]
public class State : ScriptableObject
{
    public StateAction[] enterActions;
    public StateAction[] updateActions;
    public Transition[] transitions;
    public AudioClip enterSFX;

    public void EnterState(StateController controller)
    {
        foreach (StateAction a in enterActions)
        {
            a.Act(controller);
        }
    }

    public void UpdateState(StateController controller)
    {
        foreach (StateAction a in updateActions)
        {
            a.Act(controller);
        }
        CheckTransitions(controller);
    }

    void CheckTransitions(StateController controller)
    {
        foreach (Transition t in transitions)
        {
            if (t.decision == null) continue;
            State next = t.decision.Decide(controller) ? t.trueState : t.falseState;
            if (next != null && next != controller.remainState)
            {
                controller.TransitionToState(next);
                return;
            }
        }
    }
}
