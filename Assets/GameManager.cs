using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public UnityEvent gameOver;
    
    // Private
    int score = 0;

    void Start()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1.0f;
        gameStart.Invoke();
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
}
