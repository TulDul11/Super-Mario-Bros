using UnityEngine;

public interface IPowerUp
{
    void DestroyPowerUp();
    void SpawnPowerUp();
    void ApplyPowerUp(MonoBehaviour i);

    PowerUpType powerUpType { get; }
    bool hasSpawned { get; }
}

public interface IPowerupApplicable
{
    public void RequestPowerUpEffect(IPowerUp i);
}