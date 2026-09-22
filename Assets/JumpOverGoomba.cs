using System;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class JumpOverGoomba : MonoBehaviour
{
    [System.NonSerialized] public int score = 0;

    public Transform enemyLocation;
    public TextMeshProUGUI scoreText;
    private bool onGroundState;

    private bool countScoreState = false;
    public UnityEngine.Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState = true;
    }

    private bool onGroundCheck()
    {
        return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown("space") && onGroundCheck())
        {
            onGroundState = false;
            countScoreState = true;
        }

        if (!onGroundState && countScoreState)
        {
            if (Math.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
                score += 100;
                scoreText.text = "Score: " + score.ToString();
            }
        }
    }
}
