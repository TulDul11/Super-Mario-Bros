using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D enemyBody;

    private float maxOffset = 5.0f;
    private float enemyPatrolTime = 2.0f;
    private int moveRight = 1;

    private float originalX;
    private Vector2 velocity;

    void ComputeVelocity()
    {
        velocity = new(moveRight * maxOffset / enemyPatrolTime, 0);
    }

    void MoveGoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
    }

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        originalX = transform.position.x;
        ComputeVelocity();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {
            MoveGoomba();
        }
        else
        {
            moveRight *= -1;
            ComputeVelocity();
            MoveGoomba();
        }
    }
}
