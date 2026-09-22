using System;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerMovement Mario;
    public TextMeshProUGUI scoreText;
    public GameObject enemies;
    public JumpOverGoomba JumpOverGoomba;
    public Canvas ScoreCanvas;
    public Canvas GameOverCanvas;

    private void ResetGame()
    {
        Mario.marioBody.transform.position = new Vector3(0.0f, -3.0f, 0.0f);
        Mario.faceRightState = true;
        Mario.marioSprite.flipX = false;
        scoreText.text = "Score: 0";
        foreach (Transform eachChild in enemies.transform)
        {
            var enemy = eachChild.GetComponent<EnemyMovement>();
            enemy.moveRight = 1;
            enemy.ComputeVelocity();

            eachChild.transform.localPosition = enemy.startPosition;
        }
        
        JumpOverGoomba.score = 0;
        Mario.disable = false;
    }

    public void RestartButtonCallback(int input)
    {
        Debug.Log("Restart!");
        ResetGame();
        Time.timeScale = 1.0f;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
