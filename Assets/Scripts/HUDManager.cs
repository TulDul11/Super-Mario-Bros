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
    [SerializeField] GameObject highScoreText;
    [SerializeField] TextMeshProUGUI highScoreGameOverText;

    // Public
    public IntVariable gameScore;

    // Private
    int currentScore = 0;

    public void GameStart()
    {
        scoreCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);

        UpdateHighscore();
    }

    public void GameRestart()
    {
        gameScore.Value = 0;
        SetScore(0);
        scoreCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
    }

    public void SetScore(int score)
    {
        currentScore = score;
        scoreText.text = "SCORE " + score.ToString("D6");
    }

    public void GameOver()
    {
        scoreCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        gameOverScoreText.text = "SCORE " + currentScore.ToString("D6");

        UpdateHighscore();

        highScoreGameOverText.text = "HIGHSCORE " + gameScore.previousHighestValue.ToString("D6");
    }

    void UpdateHighscore()
    {
        bool hasHighscore = gameScore.previousHighestValue > 0;
        highScoreText.SetActive(hasHighscore);

        if (hasHighscore)
        {
            highScoreText.GetComponent<TextMeshProUGUI>().text = "HIGHSCORE " + gameScore.previousHighestValue.ToString("D6");
        }
    }

    void Start()
    {
        SetScore(gameScore.Value);
        UpdateHighscore();
    }
}
