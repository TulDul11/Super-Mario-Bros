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

    void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.gameRestart.RemoveListener(GameRestart);
        }
    }

    void Start()
    {
        GameManager.instance.gameRestart.AddListener(GameRestart);
    }
}
