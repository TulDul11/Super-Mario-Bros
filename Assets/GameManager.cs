using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("References")]   
    [SerializeField] PlayerMovement marioMovement;
    [SerializeField] JumpOverGoomba jumpOverGoomba;
    [SerializeField] PlayerAnimator marioAnimator;
    [SerializeField] GameObject enemies;
    [SerializeField] GameObject scoreCanvas;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverScoreText;
    
    // Private
    int score = 0;

    private void ResetGame()
    {
        scoreCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);

        marioMovement.ResetMario();
        marioAnimator.ResetMario();

        scoreText.text = "Score: 0";
        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.GetComponent<EnemyMovement>().ResetGoomba();
        }
        
        score = 0;
        marioMovement.disable = false;
    }

    public void RestartButtonCallback()
    {
        ResetGame();
        Time.timeScale = 1.0f;
    }

    public void GameOver()
    {
        scoreCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        gameOverScoreText.text = "Score: " + score.ToString();
        Time.timeScale = 0.0f;
    }

    public void AddScore()
    {
        score += 100;
        scoreText.text = "Score: " + score.ToString();
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        scoreCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
}
