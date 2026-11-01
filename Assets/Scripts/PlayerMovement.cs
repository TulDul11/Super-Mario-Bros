using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerAnimator playerAnimator;
    [SerializeField] GameConstants gameConstants;
    [SerializeField] SimpleGameEvent onGameOver;
    [SerializeField] BoolVariable marioIsSuper;
    [SerializeField] BoolVariable marioIsDead;
    [SerializeField] BoolVariable marioHit;
    [SerializeField] BoolVariable marioIsInvincible;
    [SerializeField] StateController marioState;
    [SerializeField] BuffStateController buffState;

    [Header("Box Cast")]
    [SerializeField] Vector2 boxSize;
    [SerializeField] float maxDistance;
    [SerializeField] LayerMask layerMask;

    [Header("Stomp")]
    [SerializeField] float stompBounce = 10f;

    [Header("Super Mario")]
    [SerializeField] Sprite smallSprite;
    [SerializeField] Sprite superSprite;
    [SerializeField] Vector2 smallColliderSize;
    [SerializeField] Vector2 smallColliderOffset;
    [SerializeField] Vector2 superColliderSize;
    [SerializeField] Vector2 superColliderOffset;

    // Public (To other scripts)
    [NonSerialized] public bool faceRightState = true;
    [NonSerialized] public bool jumpPressed = false;
    [NonSerialized] public bool alive = true;
    [NonSerialized] public bool isSkid = false;
    [NonSerialized] public bool isSuper = false;

    // Private
    Rigidbody2D marioBody;
    Vector3 startPosition;
    float moveInput;
    bool holdingJump;
    SpriteRenderer marioSprite;
    BoxCollider2D marioCollider;
    float speed;
    float maxSpeed;
    float deathImpulse;
    float upSpeed;
    float riseGravity;
    float fallMultiplier;
    float lowJumpMultiplier;

    public bool IsGrounded()
    {
        Vector2 origin = (Vector2)transform.position;
        return Physics2D.BoxCast(origin, boxSize, 0f, Vector2.down, maxDistance, layerMask);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy") || marioIsDead.Value)
        {
            return;
        }

        if (marioIsInvincible.Value)
        {
            return;
        }

        if (marioBody.linearVelocity.y <= 0 && transform.position.y > other.transform.position.y + 0.5f)
        {
            return;
        }

        if (marioIsSuper.Value)
        {
            marioIsSuper.Value = false;
            marioHit.Value = true;
        } 
        else
        {
            marioIsDead.Value = true;
        }
    }

    public void Die()
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
        marioIsSuper.Value = false;
        marioIsDead.Value = false;
        marioHit.Value = false;
        marioIsInvincible.Value = false;

        transform.position = startPosition;
        marioBody.linearVelocity = Vector2.zero;
        faceRightState = true;
        alive = true;
        GetComponent<Collider2D>().enabled = true;
        
        marioState.ResetState();
        buffState.ResetState();

        SetSmallForm();
        playerAnimator.SetSuperAnimatorInstant(false);
    }

    void PlayDeathImpulse()
    {
        marioBody.gravityScale = riseGravity;
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    void GameOverScene()
    {
        onGameOver.Raise();
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

    public void MakeSuper()
    {
        if (isSuper)
        {
            return;
        }

        isSuper = true;
        marioSprite.sprite = superSprite;
        marioCollider.size = superColliderSize;
        marioCollider.offset = superColliderOffset;
        playerAnimator.SetSuperAnimator(true);
    }

    public void MakeSuperInstant()
    {
        isSuper = true;
        marioCollider.size = superColliderSize;
        marioCollider.offset = superColliderOffset;
        playerAnimator.SetSuperAnimatorInstant(true);
    }

    public void SetSmallForm()
    {
        isSuper = false;
        marioSprite.sprite = smallSprite;
        marioCollider.size = smallColliderSize;
        marioCollider.offset = smallColliderOffset;
    }

    public void Shrink()
    {
        SetSmallForm();
        playerAnimator.SetSuperAnimator(false);
    }

    void Awake()
    {
        startPosition = transform.position;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponentInChildren<SpriteRenderer>();
        marioCollider = GetComponent<BoxCollider2D>();
        
        marioIsDead.Value = false;
        marioHit.Value = false;
        marioIsInvincible.Value = false;

        speed = gameConstants.speed;
        maxSpeed = gameConstants.maxSpeed;
        upSpeed = gameConstants.upSpeed;
        deathImpulse = gameConstants.deathImpulse;
        riseGravity = gameConstants.riseGravity;
        fallMultiplier = gameConstants.fallMultiplier;
        lowJumpMultiplier = gameConstants.lowJumpMultiplier;
    }

    void Update()
    {
        if (!alive)
        {
            return;
        }

        if (Time.timeScale == 0f)
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

        ApplyJumpGravity();

        if (jumpPressed)
        {
            Jump();
            jumpPressed = false;
        }
    }
}
