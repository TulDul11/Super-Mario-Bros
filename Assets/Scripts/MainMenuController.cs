using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] IntVariable gameScore;
    [SerializeField] TextMeshProUGUI highscoreText;

    void Start()
    {
        Time.timeScale = 1f;
        gameScore.Value = 0;
        UpdateHighscore();
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Single);
    }

    public void ResetHighscore()
    {
        gameScore.ResetHighestValue();
        UpdateHighscore();
    }

    public void QuitGame() => Application.Quit();

    void UpdateHighscore()
    {
        highscoreText.text = "HIGHSCORE " + gameScore.previousHighestValue.ToString("D6");
    }
}