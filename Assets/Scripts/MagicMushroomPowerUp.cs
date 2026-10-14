using System;
using Unity.VisualScripting;
using UnityEngine;

public class MagicMushroomPowerUp : BasePowerup
{
    [Header("Movement")]
    [SerializeField] float moveForce = 3f;
    [SerializeField] float emergeForce = 2f;

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
        GetComponent<Collider2D>().enabled = false;
        rigidBody.AddForce(Vector2.up * emergeForce, ForceMode2D.Impulse);
        Invoke(nameof(StartMoving), 0.4f);
    }

    void StartMoving()
    {
        GetComponent<Collider2D>().enabled = true;
        spawned = true;
        rigidBody.linearVelocity = Vector2.zero;
    }

    public override void ApplyPowerUp(MonoBehaviour i)
    {
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
