using UnityEngine;

public class StateController : MonoBehaviour
{
    [Header("FSM")]
    [SerializeField] State startState;
    [SerializeField] public State remainState;
    [SerializeField] State currentState;

    [Header("References")]
    [SerializeField] public PlayerMovement player;
    [SerializeField] AudioSource stateAudio;
    [SerializeField] BoolVariable marioIsSuper;
    [SerializeField] public PlayerAnimator animator;

    // Public
    [HideInInspector] public float stateTimeElapsed;

    void Start()
    {
        ResetState();
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    public void TransitionToState(State nextState)
    {
        if (nextState == remainState || nextState == currentState)
        {
            return;
        }
        currentState = nextState;
        stateTimeElapsed = 0f;
        EnterCurrent();
    }

    void EnterCurrent()
    {
        if (currentState == null)
        {
            return;
        }
        if (currentState.enterSFX != null && stateAudio != null)
        {
            stateAudio.PlayOneShot(currentState.enterSFX);
        }
        currentState.EnterState(this);
    }

    public void ResetState()
    {
        currentState = startState;
        stateTimeElapsed = 0f;
        if (currentState != null)
        {
            currentState.EnterState(this);
        }
    }

    public bool CheckIfCountDownElasped(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return stateTimeElapsed >= duration;
    }
}
