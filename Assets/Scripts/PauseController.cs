using UnityEngine;

public class PauseController : MonoBehaviour
{
    [Header("Pause Canvas")]
    [SerializeField] GameObject pausePanel;

    // Private
    bool isPaused;

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        pausePanel.SetActive(isPaused);

        if (isPaused)
        {
            AudioManager.instance.PauseGame();
        }
        else
        {
            AudioManager.instance.UnPauseGame();
        }
    }
}
