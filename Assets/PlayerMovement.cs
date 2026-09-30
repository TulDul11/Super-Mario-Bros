using UnityEngine;
using System;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerMovement : MonoBehaviour
{
    [NonSerialized] public Rigidbody2D marioBody;
    [NonSerialized] public SpriteRenderer marioSprite;
    public GameManager gameManager;

    public float speed = 150.0f;
    public float maxSpeed = 5.0f;
    public float upSpeed = 15.0f;
    public float riseGravity = 3.0f;
    public float fallMultiplier = 1.8f;
    public float lowJumpMultiplier = 2.5f;
    
    [NonSerialized] public bool faceRightState = true;
    [NonSerialized] public bool disable = false;

    [SerializeField] Vector2 boxSize;
    [SerializeField] float maxDistance;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Vector2 boxOffset;

    [NonSerialized] public bool jumpPressed = false;
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
        Vector2 origin = (Vector2)transform.position + boxOffset;
        return Physics2D.BoxCast(origin, boxSize, 0f, Vector2.down, maxDistance, layerMask);
    }

    void OnDrawGizmos()
    {
        Vector2 origin = (Vector2)transform.position + boxOffset;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(origin + Vector2.down * maxDistance, boxSize);
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

        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
        }

        if (Input.GetKeyDown("d") && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
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
            marioBody.linearVelocity = Vector2.zero;
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
