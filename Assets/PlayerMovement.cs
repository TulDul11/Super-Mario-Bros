using UnityEngine;
using TMPro;
using UnityEditor.PackageManager.Requests;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    public TextMeshProUGUI scoreText;
    public GameObject enemies;
    public JumpOverGoomba JumpOverGoomba;

    public float speed = 150;
    public float maxSpeed = 5;
    public float upSpeed = 15;
    private bool onGroundState = false;
    private bool faceRightState = true;
    private bool disable = false;

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

    private void ResetGame()
    {
        marioBody.transform.position = new Vector3(0.0f, -3.0f, 0.0f);
        faceRightState = true;
        marioSprite.flipX = false;
        scoreText.text = "Score: 0";
        foreach (Transform eachChild in enemies.transform)
        {
            var enemy = eachChild.GetComponent<EnemyMovement>();
            enemy.moveRight = 1;
            enemy.ComputeVelocity();

            eachChild.transform.localPosition = enemy.startPosition;
        }
        
        JumpOverGoomba.score = 0;
    }

    public void RestartButtonCallback(int input)
    {
        Debug.Log("Restart!");
        ResetGame();
        Time.timeScale = 1.0f;
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
