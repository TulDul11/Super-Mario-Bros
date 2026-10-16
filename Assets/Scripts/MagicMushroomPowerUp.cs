using UnityEngine;
using System.Collections;

public class MagicMushroomPowerUp : BasePowerUp
{
    [Header("Movement")]
    [SerializeField] float moveForce = 3f;
    [SerializeField] float emergeForce = 2f;
    [SerializeField] float emergeDistance = 1f;
    [SerializeField] float emergeTime = 0.5f;
    [SerializeField] int normalSortingOrder = 0;

    protected override void Awake()
    {
        base.Awake();

        this.type = PowerUpType.MagicMushroom;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && spawned)
        {
            ApplyPowerUp(col.gameObject.GetComponent<PlayerMovement>());
            DestroyPowerUp();
        }
        else if (col.gameObject.layer == 10)
        {
            if (spawned)
            {
                goRight = !goRight;
                rigidBody.AddForce(Vector2.right * moveForce * (goRight ? 1 : -1), ForceMode2D.Impulse);
            }
        }
    }

    public override void SpawnPowerUp()
    {
        StartCoroutine(SpawnSequence());
    }

    IEnumerator SpawnSequence()
    {
        Vector3 start = transform.position;
        Vector3 end = start + Vector3.up * emergeDistance;

        for (float t = 0f; t < emergeTime; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(start, end, t / emergeTime);
            yield return null;
        }
        transform.position = end;

        GetComponent<SpriteRenderer>().sortingOrder = normalSortingOrder;
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForFixedUpdate();

        GetComponent<Collider2D>().enabled = true;
        spawned = true;
    }

    public override void ApplyPowerUp(MonoBehaviour i)
    {
        AudioManager.instance.PlayPowerUpSound();
        GameManager.instance.AddScore(200);
        if (i is PlayerMovement mario)
        {
            mario.MakeSuper();
        }
    }

    void FixedUpdate()
    {
        if (!spawned)
        {
            return;
        }

        rigidBody.linearVelocity = new Vector2(moveForce * (goRight ? 1 : -1), rigidBody.linearVelocity.y);
    }
}
