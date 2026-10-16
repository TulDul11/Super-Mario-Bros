using System;
using UnityEngine;

public class JumpOverGoomba : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerMovement mario;
    [SerializeField] GameManager gameManager;
    [SerializeField] Transform enemyLocation;

    [Header("Settings")]
    [SerializeField] bool working = true;

    // Private
    bool countScoreState = false;

    void FixedUpdate()
    {
        if (!working)
        {
            return;
        }
        
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
                gameManager.AddScore(100);
            }
        }
    }
}
