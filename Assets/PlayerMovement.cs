using UnityEngine;
using TMPro;
using UnityEditor.PackageManager.Requests;

public class PlayerMovement : MonoBehaviour
{
    [System.NonSerialized] public Rigidbody2D marioBody;
    [System.NonSerialized] public SpriteRenderer marioSprite;

    public float speed = 150;
    public float maxSpeed = 5;
    public float upSpeed = 15;
    [System.NonSerialized] public bool onGroundState = false;
    [System.NonSerialized] public bool faceRightState = true;
    [System.NonSerialized] public bool disable = false;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Goomba!");
            Time.timeScale = 0.0f;
            disable = true;
        }
    }

    void Start()
    {
        Application.targetFrameRate = 30;
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
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new(moveHorizontal, 0);

            if (marioBody.linearVelocity.magnitude < maxSpeed)
            {
                marioBody.AddForce(movement*speed);
            }
        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            marioBody.linearVelocity = Vector2.zero;
        }

        if (Input.GetKeyDown("space") && onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
        }
    }
}
