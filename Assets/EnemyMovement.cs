using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float maxOffset = 5.0f;
    [SerializeField] float enemyPatrolTime = 2.0f;

    // Private
    Rigidbody2D enemyBody;
    float originalX;
    Vector2 velocity;
    int moveRight = 1;
    Vector3 startPosition;

    public void ComputeVelocity()
    {
        velocity = new(moveRight * maxOffset / enemyPatrolTime, 0);
    }

    void MoveGoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    public void ResetGoomba()
    {
        moveRight = 1;
        transform.position = startPosition;
        originalX = startPosition.x;
        ComputeVelocity();
    }

    void Awake()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        originalX = transform.position.x;
        ComputeVelocity();
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
