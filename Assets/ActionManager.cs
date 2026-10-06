using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActionManager : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent jump;
    public UnityEvent<bool> jumpHold;
    public UnityEvent<float> moveCheck;

    public void OnJumpAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jump.Invoke();
        }
    }

    public void OnJumpHoldAction(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            jumpHold.Invoke(true);
        }
        else if (context.canceled)
        {
            jumpHold.Invoke(false);
        }
    }

    public void OnMoveAction(InputAction.CallbackContext context)
    {
        moveCheck.Invoke(context.ReadValue<float>());
    }
}
