using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // Events
    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public UnityEvent gameOver;


    public IntVariable gameScore;
    public int CurrentScore => gameScore.Value;
    public bool marioIsSuper = false;
    

    void Start()
    {
        gameScore.Value = 0;
        Application.targetFrameRate = 60;
        Time.timeScale = 1.0f;
        gameStart.Invoke();
        SceneManager.activeSceneChanged += SceneSetup;
    }

    public void GameRestart()
    {
        gameScore.Value = 0;
        SetScore(gameScore.Value);
        gameRestart.Invoke();
        Time.timeScale = 1.0f;
    }

    public void AddScore(int n)
    {
        gameScore.ApplyChange(n);
        SetScore(gameScore.Value);
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
    }

    void OnDestroy()
    {
        SceneManager.activeSceneChanged -= SceneSetup;
    }
}
