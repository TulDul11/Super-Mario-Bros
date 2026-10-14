using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public UnityEvent gameOver;

    public int CurrentScore => score;

    // Private
    int score = 0;

    void Start()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1.0f;
        gameStart.Invoke();
        SceneManager.activeSceneChanged += SceneSetup;
    }

    public void GameRestart()
    {
        score = 0;
        SetScore(score);
        gameRestart.Invoke();
        Time.timeScale = 1.0f;
    }

    public void AddScore(int n)
    {
        score += n;
        SetScore(score);
    }

    public void SetScore(int newScore)
    {
        scoreChange.Invoke(newScore);
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOver.Invoke();
    }

    public void SceneSetup(Scene current, Scene next)
    {
        gameStart.Invoke();
        SetScore(score);
    }

    void OnDestroy()
    {
        SceneManager.activeSceneChanged -= SceneSetup;
    }
}
