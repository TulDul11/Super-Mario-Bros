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

    void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.gameRestart.RemoveListener(ResetMario);
        }
    }

    void Awake()
    {
        marioSprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        GameManager.instance.gameRestart.AddListener(ResetMario);
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
                marioAnimator.SetTrigger("onSkid");
                marioMovement.isSkid = false;
        
        marioAnimator.SetBool("onGround", grounded);
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.linearVelocity.x));
        marioAnimator.SetFloat("ySpeed", marioBody.linearVelocity.y);

        if (Time.time < flickerUntil)
    {
        if (Time.time >= nextFlick)
        {
            visible = !visible;
            marioSprite.enabled = visible;

            if (!visible)
            {
                flickerShowsSuper = !flickerShowsSuper;
                marioAnimator.runtimeAnimatorController =
                    flickerShowsSuper ? superController : smallController;
            }

            nextFlick = Time.time + 0.06f;
        }
        }
        else if (!visible || marioAnimator.runtimeAnimatorController !=
                (targetIsSuper ? superController : smallController))
        {
            visible = true;
            marioSprite.enabled = true;
            marioAnimator.runtimeAnimatorController =
                targetIsSuper ? superController : smallController;
        }
    }
}
