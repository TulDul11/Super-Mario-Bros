using System;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerMovement mario;
    public TextMeshProUGUI scoreText;
    public GameObject enemies;
    public JumpOverGoomba jumpOverGoomba;
    public GameObject scoreCanvas;
    public GameObject gameOverCanvas;
    public TextMeshProUGUI gameOverScoreText;

    private void ResetGame()
    {
        scoreCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);

        mario.marioBody.transform.position = new Vector3(0.0f, -3.0f, 0.0f);
        mario.marioBody.linearVelocity = new Vector2(0.0f, 0.0f);
        mario.faceRightState = true;
        mario.marioSprite.flipX = false;

        scoreText.text = "Score: 0";
        foreach (Transform eachChild in enemies.transform)
        {
            var enemy = eachChild.GetComponent<EnemyMovement>();
            enemy.moveRight = 1;
            enemy.ComputeVelocity();

            eachChild.transform.localPosition = enemy.startPosition;
        }
        
        jumpOverGoomba.score = 0;
        mario.disable = false;
    }

    public void RestartButtonCallback(int input)
    {
        ResetGame();
        Time.timeScale = 1.0f;
    }

    public void GameOver()
    {
        scoreCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        gameOverScoreText.text = "Score: " + jumpOverGoomba.score.ToString();
        Time.timeScale = 0.0f;
    }

    void Start()
    {
        scoreCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
