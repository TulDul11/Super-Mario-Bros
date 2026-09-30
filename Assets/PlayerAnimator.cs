using UnityEngine;

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
        marioAnimator.SetTrigger("gameRestart");
    }
    public void PlayDeath()
    {
        marioAnimator.Play("mario-die");
    }

    void Awake()
    {
        marioSprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        marioBody = marioMovement.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bool grounded = marioMovement.IsGrounded();
        marioSprite.flipX = !marioMovement.faceRightState;

        if (marioMovement.isSkid)
                marioAnimator.SetTrigger("onSkid");
                marioMovement.isSkid = false;
        
        marioAnimator.SetBool("onGround", grounded);
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.linearVelocity.x));
    }
}
