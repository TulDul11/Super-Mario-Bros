using UnityEngine;

public abstract class BasePowerUp : MonoBehaviour, IPowerUp
{
    public PowerUpType type;
    public bool spawned = false;
    protected bool consumed = false;
    protected bool goRight = true;
    protected Rigidbody2D rigidBody;

    protected virtual void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public PowerUpType powerUpType => type;
    public bool hasSpawned => spawned;

    public void DestroyPowerUp()
    {
        Destroy(this.gameObject);
    }

    public abstract void SpawnPowerUp();
    public abstract void ApplyPowerUp(MonoBehaviour i);
}