using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameManager gameManager;

    [Header("Movement")]
    [SerializeField] float speed = 150.0f;
    [SerializeField] float maxSpeed = 5.0f;

    [Header("Jump")]
    [SerializeField] float upSpeed = 15.0f;
    [SerializeField] float riseGravity = 3.0f;
    [SerializeField] float fallMultiplier = 1.8f;
    [SerializeField] float lowJumpMultiplier = 2.5f;

    [Header("Box Cast")]
    [SerializeField] Vector2 boxSize;
    [SerializeField] float maxDistance;
    [SerializeField] LayerMask layerMask;

    // Public (To other scripts)
    [NonSerialized] public Rigidbody2D marioBody;
    [NonSerialized] public SpriteRenderer marioSprite;
    [NonSerialized] public bool faceRightState = true;
    [NonSerialized] public bool disable = false;
    [NonSerialized] public bool jumpPressed = false;

    // Private
    private bool moveReleased = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameManager.GameOver();
            disable = true;
        }
    }

    public bool IsGrounded()
    {
        Vector2 origin = (Vector2)transform.position;
        return Physics2D.BoxCast(origin, boxSize, 0f, Vector2.down, maxDistance, layerMask);
    }

    void OnDrawGizmos()
    {
        Vector2 origin = (Vector2)transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(origin + Vector2.down * maxDistance, boxSize);
    }

    public void ResetMario()
    {
        marioBody.transform.position = new Vector3(0.0f, -3.0f, 0.0f);
        marioBody.linearVelocity = new Vector2(0.0f, 0.0f);
        faceRightState = true;
        marioSprite.flipX = false;
    }

    void Start()
    {
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (disable)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A) && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.D) && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            moveReleased = true;
        }
    }

    void FixedUpdate()
    {
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
