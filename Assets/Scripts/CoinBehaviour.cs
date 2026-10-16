using UnityEngine;

public class CoinBehaviour : BasePowerUp
{
    [Header("Coin Behaviour")]
    [SerializeField] float lifetime = 1.5f;
    [SerializeField] float coinImpulse = 8f;
    [SerializeField] int scoreValue = 50;
    

    protected override void Awake()
    {
        base.Awake();
        this.type = PowerUpType.Coin;
    }

    public override void SpawnPowerUp()
    {
        spawned = true;
        rigidBody.AddForce(Vector2.up * coinImpulse, ForceMode2D.Impulse);
        ApplyPowerUp(null);
        Invoke(nameof(DestroyPowerUp), lifetime);
    }

    public override void ApplyPowerUp(MonoBehaviour i)
    {
        GameManager.instance.AddScore(scoreValue);
    }
}
