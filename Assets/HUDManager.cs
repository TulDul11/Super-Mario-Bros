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

    void OnDestroy()
    {
        if (GameManager.instance == null)
        {
            return;
        }
        
        GameManager.instance.gameStart.RemoveListener(GameStart);
        GameManager.instance.gameRestart.RemoveListener(GameRestart);
        GameManager.instance.scoreChange.RemoveListener(SetScore);
        GameManager.instance.gameOver.RemoveListener(GameOver);
    }

    void Awake()
    {
        GameManager.instance.gameStart.AddListener(GameStart);
        GameManager.instance.gameRestart.AddListener(GameRestart);
        GameManager.instance.scoreChange.AddListener(SetScore);
        GameManager.instance.gameOver.AddListener(GameOver);
    }

    void Start()
    {
        SetScore(GameManager.instance.CurrentScore);
    }
}
