using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameManager gameManager;
    [SerializeField] PlayerAnimator playerAnimator;

    [Header("Movement")]
    [SerializeField] float speed = 150.0f;
    [SerializeField] float maxSpeed = 5.0f;

    [Header("Jump")]
    [SerializeField] public float upSpeed = 15.0f;
    [SerializeField] float riseGravity = 3.0f;
    [SerializeField] float fallMultiplier = 1.8f;
    [SerializeField] float lowJumpMultiplier = 2.5f;

    [Header("Box Cast")]
    [SerializeField] Vector2 boxSize;
    [SerializeField] float maxDistance;
    [SerializeField] LayerMask layerMask;

    [Header("Death")]
    [SerializeField] float deathImpulse = 20.0f;

    // Public (To other scripts)
    [NonSerialized] public bool faceRightState = true;
    [NonSerialized] public bool jumpPressed = false;
    [NonSerialized] public bool alive = true;
    [NonSerialized] public bool isSkid = false;

    // Private
    private bool moveReleased = false;
    Rigidbody2D marioBody;
    Vector3 startPosition;

    public bool IsGrounded()
    {
        Vector2 origin = (Vector2)transform.position;
        return Physics2D.BoxCast(origin, boxSize, 0f, Vector2.down, maxDistance, layerMask);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && alive)
        {
            marioBody.linearVelocity = new Vector2(0.0f, 0.0f);
            playerAnimator.PlayDeath();
            GetComponent<Collider2D>().enabled = false;
            alive = false;
        }
    }

    void OnDrawGizmos()
    {
        Vector2 origin = (Vector2)transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(origin + Vector2.down * maxDistance, boxSize);
    }

    public void ResetMario()
    {
        transform.position = startPosition;
        marioBody.linearVelocity = new Vector2(0.0f, 0.0f);
        faceRightState = true;
        alive = true;
        GetComponent<Collider2D>().enabled = true;
    }

    void PlayDeathImpulse()
    {
        marioBody.gravityScale = riseGravity;
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    void GameOverScene()
    {
        gameManager.GameOver();
    }

    void Awake()
    {
        startPosition = transform.position;
        marioBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!alive)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A) && faceRightState)
        {
            faceRightState = false;
            isSkid = marioBody.linearVelocity.x > 0.1f && IsGrounded();
        }

        if (Input.GetKeyDown(KeyCode.D) && !faceRightState)
        {
            faceRightState = true;
            isSkid = marioBody.linearVelocity.x < -0.1f && IsGrounded();
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            moveReleased = true;
        }
    }

    void FixedUpdate()
    {
        if (!alive)
        {
            return;
        }

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new(moveHorizontal, 0);

            if (Math.Abs(marioBody.linearVelocity.x) < maxSpeed)
            {
                marioBody.AddForce(movement*speed);
            }
        }

        if (moveReleased)
        {
            marioBody.linearVelocity = new Vector2(0f, marioBody.linearVelocity.y);
            moveReleased = false;
        }

        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            jumpPressed = true;
        }

        if (jumpPressed)
        {
            marioBody.linearVelocity = new Vector2(marioBody.linearVelocity.x, 0f);
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            jumpPressed = false;
        }

        bool holdingJump = Input.GetKey(KeyCode.Space);
        float velocityY = marioBody.linearVelocity.y;

        if (velocityY > 0 && !holdingJump)
            marioBody.gravityScale = riseGravity * lowJumpMultiplier;
        else if (velocityY < 0)
            marioBody.gravityScale = riseGravity * fallMultiplier;
        else
            marioBody.gravityScale = riseGravity;
    
    }
}
