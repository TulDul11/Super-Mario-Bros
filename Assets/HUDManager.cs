using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] GameObject scoreCanvas;
    [SerializeField] GameObject gameOverCanvas;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverScoreText;

    // Private
    int currentScore = 0;

    public void GameStart()
    {
        scoreCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
    }

    public void GameRestart()
    {
        scoreCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
    }

    public void SetScore(int score)
    {
        currentScore = score;
        scoreText.text = "Score: " + score.ToString();
    }

    public void GameOver()
    {
        scoreCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        gameOverScoreText.text = "Score: " + currentScore.ToString();
    }
}
