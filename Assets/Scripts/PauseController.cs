using UnityEngine;

public class PauseController : MonoBehaviour
{
    [Header("Pause Canvas")]
    [SerializeField] GameObject pausePanel;
    [SerializeField] AudioManager audioManager;

    // Private
    bool isPaused;

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        pausePanel.SetActive(isPaused);

        if (isPaused)
        {
            audioManager.PauseGame();
        }
        else
        {
            audioManager.UnPauseGame();
        }
    }
}
