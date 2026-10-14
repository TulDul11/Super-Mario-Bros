using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator goombaAnimator;
    [SerializeField] GameConstants gameConstants;

    // Private
    Rigidbody2D enemyBody;
    float originalX;
    Vector2 velocity;
    int moveRight = 1;
    Vector3 startPosition;
    bool stomped = false;
    float maxOffset;
    float enemyPatrolTime;

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
        if (stomped)
        {
            goombaAnimator.SetTrigger("stomped");
        }

        gameObject.SetActive(true);
        stomped = false;
        enabled = true;
        GetComponent<Collider2D>().enabled = true;

        moveRight = 1;
        transform.position = startPosition;
        originalX = startPosition.x;
        ComputeVelocity();
    }

    public void Stomped()
    {
        if (stomped)
        {
            return;
        }

        foreach (var col in GetComponentsInChildren<Collider2D>())
            col.enabled = false;

        stomped = true;

        GameManager.instance.AddScore(100);

        goombaAnimator.SetTrigger("stomped");
        enabled = false;
        Invoke(nameof(Hide), 0.5f);

    }

    void Hide()
    {
        gameObject.SetActive(false);
    }

    void Awake()
    {
        maxOffset = gameConstants.goombaMaxOffset;
        enemyPatrolTime = gameConstants.goombaPatrolTime;
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
