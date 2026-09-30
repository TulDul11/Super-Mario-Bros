using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerMovement marioMovement;
    [SerializeField] Animator marioAnimator;

    // Private
    SpriteRenderer marioSprite;
    Rigidbody2D marioBody;
    bool jumpPressed = false;

    public void ResetMario()
    {
        marioSprite.flipX = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") && !marioMovement.IsGrounded())
        {
            marioAnimator.SetBool("onGround", marioMovement.IsGrounded());
        }
    }

    void Awake()
    {
        marioSprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
        marioAnimator.SetBool("onGround", marioMovement.IsGrounded());
        marioBody = marioMovement.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        marioSprite.flipX = !marioMovement.faceRightState;

        if (Input.GetKeyDown(KeyCode.A) && marioMovement.faceRightState)
        {
            if (marioBody.linearVelocity.x > 0.1f)
                marioAnimator.SetTrigger("onSkid");
        }

        if (Input.GetKeyDown(KeyCode.D) && !marioMovement.faceRightState)
        {
            if (marioBody.linearVelocity.x < -0.1f)
                marioAnimator.SetTrigger("onSkid");
        }

        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.linearVelocity.x));

        if (Input.GetKeyDown(KeyCode.Space) && marioMovement.IsGrounded())
        {
            jumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        if (jumpPressed)
        {
            marioAnimator.SetBool("onGround", marioMovement.IsGrounded());
        }
    }
}
