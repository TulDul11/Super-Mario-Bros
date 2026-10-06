using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator goombaAnimator;
    
    [Header("Movement")]
    [SerializeField] float maxOffset = 5.0f;
    [SerializeField] float enemyPatrolTime = 2.0f;

    [Header("Stomp")]
    [SerializeField] UnityEvent<int> onStomped;
    [SerializeField] int scoreValue = 100;


    // Private
    Rigidbody2D enemyBody;
    float originalX;
    Vector2 velocity;
    int moveRight = 1;
    Vector3 startPosition;
    bool stomped = false;

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
        onStomped.Invoke(scoreValue);
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
