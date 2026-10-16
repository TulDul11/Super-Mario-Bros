using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    public float speed;
    public float maxSpeed;
    public float upSpeed;
    public float deathImpulse;
    public float riseGravity;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    public float goombaPatrolTime;
    public float goombaMaxOffset;
}
