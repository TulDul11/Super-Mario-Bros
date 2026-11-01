using UnityEngine;

public class RestartButtonController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SimpleGameEvent onGameRestart;
    [SerializeField] IntVariable gameScore;

    public void ButtonClick()
    {
        gameScore.Value = 0;
        onGameRestart.Raise();
    }
}
