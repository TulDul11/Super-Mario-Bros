using System;
using TMPro;
using UnityEngine;

public class JumpOverGoomba : MonoBehaviour
{
    public PlayerMovement mario;
    public GameManager gameManager;

    public Transform enemyLocation;
    public TextMeshProUGUI scoreText;

    private bool countScoreState = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (mario.IsGrounded())
        {
            countScoreState = true;
            return;
        }

        if (!mario.IsGrounded() && countScoreState)
        {
            if (Math.Abs(mario.transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
                gameManager.score += 100;
                scoreText.text = "Score: " + gameManager.score.ToString();
            }
        }
    }
}
