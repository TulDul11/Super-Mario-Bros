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

    [Header("Stomp")]
    [SerializeField] float stompBounce = 10f;

    // Public (To other scripts)
    [NonSerialized] public bool faceRightState = true;
    [NonSerialized] public bool jumpPressed = false;
    [NonSerialized] public bool alive = true;
    [NonSerialized] public bool isSkid = false;

    // Private
    Rigidbody2D marioBody;
    Vector3 startPosition;
    float moveInput;
    bool holdingJump;

    public bool IsGrounded()
    {
        Vector2 origin = (Vector2)transform.position;
        return Physics2D.BoxCast(origin, boxSize, 0f, Vector2.down, maxDistance, layerMask);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy") || !alive)
        {
            return;
        }

        if (marioBody.linearVelocity.y <= 0 && transform.position.y > other.transform.position.y + 0.5f)
        {
            return;
        }

        Die();
    }

    void Die()
    {
        marioBody.linearVelocity = new Vector2(0.0f, 0.0f);
        playerAnimator.PlayDeath();
        GetComponent<Collider2D>().enabled = false;
        alive = false;
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

    void FlipMarioSprite(int value)
    {
        if (value == 1 && !faceRightState)
        {
            faceRightState = true;
            isSkid = marioBody.linearVelocity.x < -0.1f && IsGrounded();
        }
        else if (value == -1 && faceRightState)
        {
            faceRightState = false;
            isSkid = marioBody.linearVelocity.x > 0.1f && IsGrounded();
        }
    }

    void Move(float value)
    {
        bool sameDirection = Mathf.Sign(value) == Mathf.Sign(marioBody.linearVelocity.x);
        bool atMaxSpeed = Mathf.Abs(marioBody.linearVelocity.x) >= maxSpeed;

        if (!sameDirection || !atMaxSpeed)
            marioBody.AddForce(new Vector2(value, 0) * speed);
    }
    
    void Stop()
    {
        marioBody.linearVelocity = new Vector2(0f, marioBody.linearVelocity.y);
    }

    void Jump()
    {
        marioBody.linearVelocity = new Vector2(marioBody.linearVelocity.x, 0f);
        marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
    }

    void ApplyJumpGravity()
    {
        float velocityY = marioBody.linearVelocity.y;

        if (velocityY > 0 && !holdingJump)
            marioBody.gravityScale = riseGravity * lowJumpMultiplier;
        else if (velocityY < 0)
            marioBody.gravityScale = riseGravity * fallMultiplier;
        else
            marioBody.gravityScale = riseGravity;
    }

    public void StompBounce()
    {
        marioBody.linearVelocity = new Vector2(marioBody.linearVelocity.x, 0f);
        marioBody.AddForce(Vector2.up * stompBounce, ForceMode2D.Impulse);
    }

    void Awake()
    {
        startPosition = transform.position;
        marioBody = GetComponent<Rigidbody2D>();
    }

    public void JumpAction()
    {
        if (alive && IsGrounded())
        {
            jumpPressed = true;
            holdingJump = true;
        }
    }

    public void JumpHoldAction(bool held)
    {
        holdingJump = held;
    }

    public void MoveAction(float value)
    {
        moveInput = value;
    }

    void Update()
    {
        if (!alive)
        {
            return;
        }

        if (moveInput > 0.1f && !faceRightState)
        {
            FlipMarioSprite(1);
        }
        else if (moveInput < -0.1f && faceRightState)
        {
            FlipMarioSprite(-1);
        }
    }

    void FixedUpdate()
    {
        if (!alive)
        {
            return;
        }

        if (Mathf.Abs(moveInput) > 0.1f)
        {
            Move(moveInput);
        }
        else if (IsGrounded() && Mathf.Abs(marioBody.linearVelocity.x) < 4f)
        {
            Stop();
        }

        if (jumpPressed)
        {
            Jump();
            jumpPressed = false;
        }

        ApplyJumpGravity();
    }
}
