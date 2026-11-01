using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerMovement marioMovement;
    [SerializeField] Animator marioAnimator;

    [Header("Super")]
    [SerializeField] AnimatorOverrideController superController;
    [SerializeField] RuntimeAnimatorController smallController;

    // Private
    SpriteRenderer marioSprite;
    Rigidbody2D marioBody;
    float flickerUntil = 0f;
    float nextFlick = 0f;
    bool visible = true;
    bool flickerShowsSuper = false;
    bool targetIsSuper = false;
    bool invincibleFlicker = false;

    public void ResetMario()
    {
        marioSprite.flipX = false;
        marioAnimator.SetTrigger("gameRestart");
    }
    public void PlayDeath()
    {
        marioAnimator.Play("mario-die");
    }

    public void SetSuperAnimator(bool isSuper)
    {
        targetIsSuper = isSuper;
        flickerUntil = Time.time + 0.6f;
        nextFlick = 0f;
    }

    public void SetSuperAnimatorInstant(bool isSuper)
    {
        targetIsSuper = isSuper;
        flickerUntil = 0f;
        visible = true;
        marioSprite.enabled = true;
        marioAnimator.runtimeAnimatorController = isSuper ? superController : smallController;
    }

    public void SetInvincibleFlicker(bool on)
    {
        invincibleFlicker = on;
        nextFlick = 0f;
        if (!on) 
        {
            visible = true;
            marioSprite.enabled = true;
        }
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
        if (Time.timeScale == 0f)
        {
            return;
        }
        
        bool grounded = marioMovement.IsGrounded();
        marioSprite.flipX = !marioMovement.faceRightState;

        if (marioMovement.isSkid)
        {
                marioAnimator.SetTrigger("onSkid");
                marioMovement.isSkid = false;
        }
        
        marioAnimator.SetBool("onGround", grounded);
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.linearVelocity.x));
        marioAnimator.SetFloat("ySpeed", marioBody.linearVelocity.y);

        bool transforming = Time.time < flickerUntil;
        if (!transforming)
        {
            RuntimeAnimatorController want = targetIsSuper ? superController : smallController;
            if (marioAnimator.runtimeAnimatorController != want)
                marioAnimator.runtimeAnimatorController = want;
        }
        if (transforming || invincibleFlicker)
        {
            if (Time.time >= nextFlick)
            {
                visible = !visible;
                marioSprite.enabled = visible;

                if (transforming && !visible)
                {
                    flickerShowsSuper = !flickerShowsSuper;
                    marioAnimator.runtimeAnimatorController =
                        flickerShowsSuper ? superController : smallController;
                }

                nextFlick = Time.time + (transforming ? 0.06f : 0.1f);
            }
        }
        else if (!visible)
        {
            visible = true;
            marioSprite.enabled = true;
        }
    }
}
