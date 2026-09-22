using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D enemyBody;

    private float maxOffset = 5.0f;
    private float enemyPatrolTime = 2.0f;
    [System.NonSerialized] public int moveRight = 1;
    [System.NonSerialized] public Vector3 startPosition = new(4.0f, -2.5f, 0.0f);

    private float originalX;
    private Vector2 velocity;

    public void ComputeVelocity()
    {
        velocity = new(moveRight * maxOffset / enemyPatrolTime, 0);
    }

    void MoveGoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
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
