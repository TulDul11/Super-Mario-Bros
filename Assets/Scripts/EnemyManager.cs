using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public void GameRestart()
    {
        foreach (EnemyMovement enemy in GetComponentsInChildren<EnemyMovement>(true))
        {
            enemy.ResetGoomba();
        }
    }
}
